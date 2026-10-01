using HanBase.Models;
using Microsoft.EntityFrameworkCore;

namespace HanBase.Data;

public class HanBaseContext : DbContext
{
    public HanBaseContext(DbContextOptions<HanBaseContext> options)
        : base(options)
    {
    }

    public DbSet<KoreanHunReading> KoreanHunReadings { get; set; } = default!;

    public DbSet<NumericValue> NumericValues { get; set; } = default!;

    public DbSet<RadicalStrokeCount> RadicalStrokeCounts { get; set; } = default!;

    public DbSet<Reading> Readings { get; set; } = default!;

    public DbSet<TrainingCharacter> TrainingCharacters { get; set; } = default!;

    public DbSet<Variant> Variants { get; set; } = default!;
}
