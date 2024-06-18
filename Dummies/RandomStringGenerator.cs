namespace ToolBX.Dummies;

internal static class RandomStringGenerator
{
    private const string LatinAlphabet = Characters.Letters + Characters.Numbers;

    internal static string Generate(int min, int max) => Generate(PseudoRandomNumberGenerator.Shared.Generate(min, max));

    internal static string Generate(int length)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < length; i++)
        {
            sb.Append(LatinAlphabet.GetRandom());
        }
        return sb.ToString();
    }
}