namespace ToolBX.Dummies;

public interface IDummyDateTimeBuilder
{
    /// <summary>
    /// Returns a <see cref="DateTime"/> in the past using the current date. Compatible with <see cref="ToolBX.TimeProvider.TimeProvider"/>.
    /// Use <see cref="Before(DateTime)"/> for more precise control.
    /// </summary>
    IDummyDateTimeBuilderLastStep BeforeNow();

    /// <summary>
    /// Returns a <see cref="DateTimeOffset"/> in the past using the current date. Compatible with <see cref="ToolBX.TimeProvider.TimeProvider"/>.
    /// Use <see cref="Before(DateTimeOffset)"/> or <see cref="Between(DateTimeOffset,DateTimeOffset)"/> for more precise control.
    /// </summary>
    IDummyDateTimeOffsetBuilderLastStep BeforeNowOffset();

    /// <summary>
    /// Returns a <see cref="DateTime"/> in the past using the current date. Compatible with <see cref="ToolBX.TimeProvider.TimeProvider"/>.
    /// Use <see cref="Before(DateTime)"/> or <see cref="Between(DateTime,DateTime)"/> for more precise control.
    /// </summary>
    IDummyDateTimeBuilderLastStep BeforeToday();

    /// <summary>
    /// Returns a <see cref="DateTimeOffset"/> in the past using the current date. Compatible with <see cref="ToolBX.TimeProvider.TimeProvider"/>.
    /// Use <see cref="Before(DateTimeOffset)"/> or <see cref="Between(DateTimeOffset,DateTimeOffset)"/> for more precise control.
    /// </summary>
    IDummyDateTimeOffsetBuilderLastStep BeforeTodayOffset();

    IDummyDateTimeBuilderLastStep Before(DateTime value);
    IDummyDateTimeOffsetBuilderLastStep Before(DateTimeOffset value);
    IDummyDateOnlyBuilder Before(DateOnly value);

    IDummyDateTimeBuilderLastStep AfterNow();
    IDummyDateTimeOffsetBuilderLastStep AfterNowOffset();

    IDummyDateTimeBuilderLastStep AfterToday();
    IDummyDateTimeOffsetBuilderLastStep AfterTodayOffset();

    IDummyDateTimeBuilderLastStep After(DateTime value);
    IDummyDateTimeOffsetBuilderLastStep After(DateTimeOffset value);
    IDummyDateOnlyBuilder After(DateOnly value);

    IDummyDateTimeBuilderLastStep Between(DateTime min, DateTime max);
    IDummyDateTimeOffsetBuilderLastStep Between(DateTimeOffset min, DateTimeOffset max);
    IDummyDateOnlyBuilder Between(DateOnly min, DateOnly max);

    DateTime Create();
    IEnumerable<DateTime> CreateMany();
    IEnumerable<DateTime> CreateMany(int amount);

    DateTimeOffset CreateOffset();
    IEnumerable<DateTimeOffset> CreateManyOffset();
    IEnumerable<DateTimeOffset> CreateManyOffset(int amount);

    DateOnly CreateDateOnly();
    IEnumerable<DateOnly> CreateManyDateOnly();
    IEnumerable<DateOnly> CreateManyDateOnly(int amount);

}

internal sealed class DummyDateTimeBuilder : IDummyDateTimeBuilder
{
    private readonly Dummy _dummy;

    internal DummyDateTimeBuilder(Dummy dummy)
    {
        _dummy = dummy;
    }

    public IDummyDateTimeBuilderLastStep BeforeNow()
    {
        var now = TimeProvider.TimeProvider.Now.UtcDateTime;
        return Between(now.AddYears(-5), now.AddMinutes(-1));
    }

    public IDummyDateTimeOffsetBuilderLastStep BeforeNowOffset()
    {
        var now = TimeProvider.TimeProvider.Now;
        return Between(now.AddYears(-5), now.AddMinutes(-1));
    }

    public IDummyDateTimeBuilderLastStep BeforeToday()
    {
        var today = TimeProvider.TimeProvider.Now.UtcDateTime;
        return Between(today.AddYears(-5), today.AddDays(-1));
    }

    public IDummyDateTimeOffsetBuilderLastStep BeforeTodayOffset()
    {
        var today = TimeProvider.TimeProvider.Now;
        return Between(today.AddYears(-5), today.AddDays(-1));
    }

    public IDummyDateTimeBuilderLastStep Before(DateTime value) => Between(DateTime.MinValue, value);

    public IDummyDateOnlyBuilder Before(DateOnly value) => Between(DateOnly.MinValue, value);

    public IDummyDateTimeBuilderLastStep AfterNow()
    {
        var now = TimeProvider.TimeProvider.Now.UtcDateTime;
        return Between(now.AddMinutes(1), now.AddYears(5));
    }

    public IDummyDateTimeOffsetBuilderLastStep AfterNowOffset()
    {
        var now = TimeProvider.TimeProvider.Now;
        return Between(now.AddMinutes(1), now.AddYears(5));
    }

    public IDummyDateTimeBuilderLastStep AfterToday()
    {
        var today = TimeProvider.TimeProvider.Now.UtcDateTime;
        return Between(today.AddDays(1), today.AddYears(5));
    }

    public IDummyDateTimeOffsetBuilderLastStep AfterTodayOffset()
    {
        var today = TimeProvider.TimeProvider.Now;
        return Between(today.AddDays(1), today.AddYears(5));
    }

    public IDummyDateTimeBuilderLastStep After(DateTime value) => Between(value, DateTime.MaxValue);

    public IDummyDateOnlyBuilder After(DateOnly value) => Between(value, DateOnly.MaxValue);

    public IDummyDateTimeBuilderLastStep Between(DateTime min, DateTime max) => new DummyDateTimeBuilderLastStep(_dummy, () =>
    {
        if (min > max)
            throw new ArgumentException(ExceptionMessages.StartDateMustBeEarlier);

        var timeSpan = max - min;
        var randomSeconds = PseudoRandomNumberGenerator.Shared.Generate(0, timeSpan.TotalSeconds);
        return min.AddSeconds(randomSeconds);
    });

    public IDummyDateTimeOffsetBuilderLastStep Before(DateTimeOffset value) => Between(DateTime.MinValue, value);

    public IDummyDateTimeOffsetBuilderLastStep After(DateTimeOffset value) => Between(value, DateTimeOffset.MaxValue);

    public IDummyDateTimeOffsetBuilderLastStep Between(DateTimeOffset min, DateTimeOffset max) => new DummyDateTimeOffsetBuilderLastStep(_dummy, () =>
    {
        if (min > max)
            throw new ArgumentException(ExceptionMessages.StartDateMustBeEarlier);

        var timeSpan = max - min;
        var randomSeconds = PseudoRandomNumberGenerator.Shared.Generate(0, timeSpan.TotalSeconds);
        return min.AddSeconds(randomSeconds);
    });

    public IDummyDateOnlyBuilder Between(DateOnly min, DateOnly max) => new DummyDateOnlyBuilder(_dummy, () =>
    {
        if (min > max)
            throw new ArgumentException(ExceptionMessages.StartDateMustBeEarlier);

        var range = max.DayNumber - min.DayNumber;
        var randomDays = PseudoRandomNumberGenerator.Shared.Generate(0, range + 1);
        return min.AddDays(randomDays);
    });

    public DateTime Create() => Between(DateTime.MinValue, DateTime.MaxValue).Create();

    public IEnumerable<DateTime> CreateMany() => CreateMany(_dummy.Options.DefaultCollectionSize);

    public IEnumerable<DateTime> CreateMany(int amount)
    {
        for (var i = 0; i < amount; i++)
            yield return Create();
    }

    public DateTimeOffset CreateOffset() => Between(DateTimeOffset.MinValue, DateTimeOffset.MaxValue).Create();

    public IEnumerable<DateTimeOffset> CreateManyOffset() => CreateManyOffset(_dummy.Options.DefaultCollectionSize);

    public IEnumerable<DateTimeOffset> CreateManyOffset(int amount)
    {
        for (var i = 0; i < amount; i++)
            yield return CreateOffset();
    }

    public DateOnly CreateDateOnly() => Between(DateOnly.MinValue, DateOnly.MaxValue).Create();

    public IEnumerable<DateOnly> CreateManyDateOnly() => CreateManyDateOnly(_dummy.Options.DefaultCollectionSize);

    public IEnumerable<DateOnly> CreateManyDateOnly(int amount)
    {
        for (var i = 0; i < amount; i++)
            yield return CreateDateOnly();
    }
}

public interface IDummyDateTimeBuilderLastStep : ISpecializedBuilder<DateTime>;

internal sealed class DummyDateTimeBuilderLastStep : SpecializedBuilder<DateTime>, IDummyDateTimeBuilderLastStep
{
    private readonly Func<DateTime> _factory;

    internal DummyDateTimeBuilderLastStep(IDummy dummy, Func<DateTime> factory) : base(dummy)
    {
        _factory = factory;
    }

    public override DateTime Create() => _factory();
}

public interface IDummyDateTimeOffsetBuilderLastStep : ISpecializedBuilder<DateTimeOffset>;

internal sealed class DummyDateTimeOffsetBuilderLastStep : SpecializedBuilder<DateTimeOffset>, IDummyDateTimeOffsetBuilderLastStep
{
    private readonly Func<DateTimeOffset> _factory;

    internal DummyDateTimeOffsetBuilderLastStep(IDummy dummy, Func<DateTimeOffset> factory) : base(dummy)
    {
        _factory = factory;
    }

    public override DateTimeOffset Create() => _factory();
}

public interface IDummyDateOnlyBuilder
{
    DateOnly Create();
    IEnumerable<DateOnly> CreateMany();
    IEnumerable<DateOnly> CreateMany(int amount);
}

internal sealed class DummyDateOnlyBuilder : IDummyDateOnlyBuilder
{
    private readonly IDummy _dummy;
    private readonly Func<DateOnly> _factory;

    internal DummyDateOnlyBuilder(IDummy dummy, Func<DateOnly> factory)
    {
        _dummy = dummy;
        _factory = factory;
    }

    public DateOnly Create() => _factory();

    public IEnumerable<DateOnly> CreateMany() => CreateMany(_dummy.Options.DefaultCollectionSize);

    public IEnumerable<DateOnly> CreateMany(int amount)
    {
        for (var i = 0; i < amount; i++)
            yield return Create();
    }
}