using Application.DTOs;
using Domain;
using FluentValidation;

namespace Application.Validators
{
    public class PostPetsValidator : AbstractValidator<PostPetsDTO>
    {
        public PostPetsValidator()
        {
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Description).NotEmpty().WithMessage("Description can not be empty or null");
            RuleFor(p => p.DogBreeds).NotEmpty();
            RuleFor(p => p.Image).NotNull().WithMessage("choose a picture");
        }
    }

    public class PetsValidator : AbstractValidator<Pets>
    {
        public PetsValidator()
        {
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Id).GreaterThan(0);
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.DogBreeds).NotEmpty();
            RuleFor(p => p.Image).NotNull().WithMessage("choose a picture");
        }
    }
}