using Application.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.V1.ShowWeather
{
    public class GetWeather : IRequest<Response<IEnumerable<WeatherForecast>>>
    {
    }
}
