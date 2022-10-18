using FluentResults;

namespace Mm0205.TestImageGenerator.Cli.Domain.Models;

/// <summary>
/// 出力インデックス
/// </summary>
public class OutputIndex
{
    private const int MinValue = 0;
    private const int MaxValue = int.MaxValue - 5000;

    private static readonly string ErrorMessage = $"出力インデックスには{0}以上の整数値を指定してください";

    /// <summary>
    /// 出力インデックス。
    /// </summary>
    public int Value { get; }

    private OutputIndex(int value)
    {
        Value = value;
    }

    /// <summary>
    /// 出力インデックスオブジェクトを作成する。
    /// </summary>
    /// <param name="count">出力インデックス。</param>
    /// <returns>結果。</returns>
    public static Result<OutputIndex> Create(int count)
    {
        if (count is < MinValue or > MaxValue)
        {
            return Result.Fail<OutputIndex>(
                new Error(ErrorMessage).WithMetadata(nameof(count), count));
        }

        return new OutputIndex(count);
    }

    public IEnumerable<OutputIndex> CreateRange(OutputFileCount count)
        => Enumerable.Range(Value, count.Value).Select(x => new OutputIndex(x));
}