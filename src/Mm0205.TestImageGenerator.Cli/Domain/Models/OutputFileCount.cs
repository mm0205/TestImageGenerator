using FluentResults;

namespace Mm0205.TestImageGenerator.Cli.Domain.Models;

/// <summary>
/// 出力ファイル数。
/// </summary>
public class OutputFileCount
{
    private const int MinValue = 1;
    private const int MaxValue = 5000;

    private static readonly string ErrorMessage = $"出力ファイル数には{MinValue}〜{MaxValue}の間の整数値を指定してください";

    /// <summary>
    /// 出力ファイル数。
    /// </summary>
    public int Value { get; }

    private OutputFileCount(int value)
    {
        Value = value;
    }

    /// <summary>
    /// 出力ファイル数オブジェクトを作成する。
    /// </summary>
    /// <param name="count">出力ファイル数。</param>
    /// <returns>結果。</returns>
    public static Result<OutputFileCount> Create(int count)
    {
        if (count is < MinValue or > MaxValue)
        {
            return Result.Fail<OutputFileCount>(
                new Error(ErrorMessage).WithMetadata(nameof(count), count));
        }

        return new OutputFileCount(count);
    }
}