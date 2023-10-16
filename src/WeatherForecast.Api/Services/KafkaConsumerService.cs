using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Newtonsoft.Json;
using WeatherForecast.Api.Models.Service;
using WeatherForecast.Api.Repositories;

namespace WeatherForecast.Api.Services
{
    public class KafkaConsumerService: IKafkaConsumerService
    {
        private readonly IWeatherTodayRepository _weatherTodayRepository;
        public KafkaConsumerService(IWeatherTodayRepository weatherTodayRepository) 
        {
            _weatherTodayRepository = weatherTodayRepository;
        }

        public async Task StartConsumeAsync()
        {
            var source = new CancellationTokenSource();
            var token = source.Token;

            var weatherTodayList = new List<WeatherToday>();
            var config = new ConsumerConfig
            {
                GroupId = "test-kafka-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = config.BootstrapServers }).Build())
                {
                    try
                    {
                        await adminClient.CreateTopicsAsync(new TopicSpecification[] {
                        new TopicSpecification { Name = "weather.forecast.topic.local", ReplicationFactor = 1, NumPartitions = 1 } });
                    }
                    catch (CreateTopicsException e)
                    {
                        Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
                    }
                }


                c.Subscribe("weather.forecast.topic.local");

                var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);
                            Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                            var weatherToday = JsonConvert.DeserializeObject<List<WeatherToday>>(cr.Message.Value);
                            await _weatherTodayRepository.DeleteAllAsync();
                            await _weatherTodayRepository.CreateAllAsync(weatherToday ?? new List<WeatherToday>());

                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }
            }

            //using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            //consumer.Subscribe("weather.forecast.topic.local");

            //while (!source.IsCancellationRequested)
            //{
            //    var consumed = consumer.Consume();
            //    if (consumed != null)
            //    {
            //        break;
            //    }
            //    weatherTodayList = JsonConvert.DeserializeObject<List<WeatherToday>>(consumed.Message.Value);
            //}
        }
    }
}
