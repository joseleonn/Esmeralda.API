using Application.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.V1.ShowWeather
{
    public class ShowWeatherHandler : IRequestHandler<GetWeather, Response<IEnumerable<WeatherForecast>>>
    {
        private static readonly string[] Summaries = new[]
       {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IMediator _mediatr;
        public ShowWeatherHandler(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task<Response<IEnumerable<WeatherForecast>>> Handle(GetWeather request, CancellationToken cancellationToken)
        {
         

            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return Response<IEnumerable<WeatherForecast>>.Success(forecasts);

        }
    }
}
