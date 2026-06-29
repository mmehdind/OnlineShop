using FluentValidation;
using OnlineShop.Admin.ViewModels.Category;

public class CreateCategoryVmValidator : AbstractValidator<CreateCategoryAdminVm>
{
    public CreateCategoryVmValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("نام دسته‌بندی الزامی است.")
            .MaximumLength(100)
            .WithMessage("نام دسته‌بندی نباید بیشتر از 100 کاراکتر باشد.");

        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithMessage("Slug الزامی است.")
            .MaximumLength(100)
            .WithMessage("Slug نباید بیشتر از 100 کاراکتر باشد.")
            .Matches(@"^[a-z0-9]+(?:-[a-z0-9]+)*$")
            .WithMessage("Slug فقط می‌تواند شامل حروف کوچک انگلیسی، اعداد و '-' باشد.");
    }
}