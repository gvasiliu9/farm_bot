using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Validations
{
    public class PlantValidator : AbstractValidator<Plant>
    {
        public PlantValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
