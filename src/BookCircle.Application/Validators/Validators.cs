using BookCircle.Application.DTOs;
using FluentValidation;

namespace BookCircle.Application.Validators;

public class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Username).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(120);
    }
}

public class ClubCreateValidator : AbstractValidator<ClubCreateDto>
{
    public ClubCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
    }
}

public class BookCreateValidator : AbstractValidator<BookCreateDto>
{
    public BookCreateValidator()
    {
        RuleFor(x => x.ClubId).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Author).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Votes).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Progress).InclusiveBetween(0, 100);
    }
}

public class ReviewCreateValidator : AbstractValidator<ReviewCreateDto>
{
    public ReviewCreateValidator()
    {
        RuleFor(x => x.ClubId).GreaterThan(0);
        RuleFor(x => x.BookId).GreaterThan(0);
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.Rating).InclusiveBetween(1, 5);
        RuleFor(x => x.Comment).NotEmpty().MaximumLength(1000);
    }
}

public class ReviewUpdateValidator : AbstractValidator<ReviewUpdateDto>
{
    public ReviewUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Rating).InclusiveBetween(1, 5);
        RuleFor(x => x.Comment).NotEmpty().MaximumLength(1000);
    }
}

public class MeetingCreateValidator : AbstractValidator<MeetingCreateDto>
{
    public MeetingCreateValidator()
    {
        RuleFor(x => x.BookId).GreaterThan(0);
        RuleFor(x => x.ClubId).GreaterThan(0);
        RuleFor(x => x.BookTitle).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Location).MaximumLength(300);
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}

public class MeetingAttendanceCreateValidator : AbstractValidator<MeetingAttendanceCreateDto>
{
    private static readonly HashSet<string> ValidStatuses =
        new(StringComparer.OrdinalIgnoreCase) { "SI", "NO", "TAL_VEZ" };

    public MeetingAttendanceCreateValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.Status)
            .NotEmpty()
            .Must(s => ValidStatuses.Contains(s))
            .WithMessage("Status must be one of: SI, NO, TAL_VEZ");
    }
}
