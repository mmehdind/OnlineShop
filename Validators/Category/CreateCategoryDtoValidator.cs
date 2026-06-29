using FluentValidation;
using OnlineShop.DTOs.Category;

namespace OnlineShop.Validators.Category;

public class CreateCategoryDtoValidator
    : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(100)
            .Matches("^[a-z0-9-]+$")
            .WithMessage(
                "Slug can only contain lowercase letters, numbers and hyphens.");
    }
}