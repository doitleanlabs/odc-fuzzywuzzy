using System.Collections.Generic;
using NUnit.Framework;
using DoiTLean.FuzzyWuzzy.Structures;

namespace DoiTLean.FuzzyWuzzy.UnitTests;

public class FuzzyWuzzyTests
{
    private FuzzyWuzzy _fuzzyWuzzy;

    [SetUp]
    public void SetUp()
    {
        _fuzzyWuzzy = new FuzzyWuzzy();
    }

    [Test]
    public void Ratio_IdenticalStrings_Returns100()
    {
        _fuzzyWuzzy.Ratio("mysimilarstring", "mysimilarstring", out int ratio);
        Assert.That(ratio, Is.EqualTo(100));
    }

    [Test]
    public void Ratio_DifferentStrings_ReturnsPartialScore()
    {
        _fuzzyWuzzy.Ratio("mysmilarstring", "myawfullysimilarstirng", out int ratio);
        Assert.That(ratio, Is.EqualTo(72));
    }

    [Test]
    public void WeightedRatio_KnownInputs_ReturnsExpectedScore()
    {
        _fuzzyWuzzy.WeightedRatio(
            "The quick brown fox jimps ofver the small lazy dog",
            "the quick brown fox jumps over the small lazy dog",
            out int ratio);
        Assert.That(ratio, Is.EqualTo(95));
    }

    [Test]
    public void Process_ExtractOne_KnownInputs_ReturnsBestMatch()
    {
        List<TextRecord> strings = new List<TextRecord>
        {
            new TextRecord("Atlanta Falcons"),
            new TextRecord("New York Jets"),
            new TextRecord("New York Giants"),
            new TextRecord("Dallas Cowboys"),
        };

        _fuzzyWuzzy.Process_ExtractOne("cowboys", strings, out ResultRecord result);

        Assert.That(result.String, Is.EqualTo("Dallas Cowboys"));
        Assert.That(result.Score, Is.EqualTo(90));
        Assert.That(result.Index, Is.EqualTo(3));
    }

    [Test]
    public void Process_ExtractOne_EmptyStrings_ReturnsDefaultResultInsteadOfThrowing()
    {
        _fuzzyWuzzy.Process_ExtractOne("cowboys", new List<TextRecord>(), out ResultRecord result);

        Assert.That(result.String, Is.Empty);
        Assert.That(result.Score, Is.EqualTo(0));
        Assert.That(result.Index, Is.EqualTo(0));
    }

    [Test]
    public void Process_ExtractOne_NullStrings_ReturnsDefaultResultInsteadOfThrowing()
    {
        _fuzzyWuzzy.Process_ExtractOne("cowboys", (List<TextRecord>)null!, out ResultRecord result);

        Assert.That(result.String, Is.Empty);
    }

    [Test]
    public void Process_ExtractOne_NullSearchTerm_ReturnsDefaultResultInsteadOfThrowing()
    {
        List<TextRecord> strings = new List<TextRecord> { new TextRecord("Dallas Cowboys") };

        _fuzzyWuzzy.Process_ExtractOne((string)null!, strings, out ResultRecord result);

        Assert.That(result.String, Is.Empty);
    }

    [Test]
    public void Process_ExtractTop_KnownInputs_ReturnsTopMatchesInOrder()
    {
        List<TextRecord> strings = new List<TextRecord>
        {
            new TextRecord("google"),
            new TextRecord("bing"),
            new TextRecord("facebook"),
            new TextRecord("linkedin"),
            new TextRecord("twitter"),
            new TextRecord("googleplus"),
            new TextRecord("bingnews"),
            new TextRecord("plexoogl"),
        };

        _fuzzyWuzzy.Process_ExtractTop("goolge", strings, 3, 0, out List<ResultRecord> results);

        Assert.That(results, Has.Count.EqualTo(3));
        Assert.That(results[0].String, Is.EqualTo("google"));
    }

    [Test]
    public void Process_ExtractTop_EmptyStrings_ReturnsEmptyListInsteadOfThrowing()
    {
        _fuzzyWuzzy.Process_ExtractTop("goolge", new List<TextRecord>(), 3, 0, out List<ResultRecord> results);
        Assert.That(results, Is.Empty);
    }

    [Test]
    public void Process_ExtractTop_NullStrings_ReturnsEmptyListInsteadOfThrowing()
    {
        _fuzzyWuzzy.Process_ExtractTop("goolge", (List<TextRecord>)null!, 3, 0, out List<ResultRecord> results);
        Assert.That(results, Is.Empty);
    }

    [Test]
    public void Process_ExtractAll_CutoffFiltersLowScores()
    {
        List<TextRecord> strings = new List<TextRecord>
        {
            new TextRecord("google"),
            new TextRecord("bing"),
            new TextRecord("googleplus"),
            new TextRecord("plexoogl"),
        };

        _fuzzyWuzzy.Process_ExtractAll("goolge", strings, 40, out List<ResultRecord> results);

        Assert.That(results.Exists(r => r.String == "bing"), Is.False);
        Assert.That(results.Exists(r => r.String == "google"), Is.True);
    }

    [Test]
    public void Process_ExtractAll_NullStrings_ReturnsEmptyListInsteadOfThrowing()
    {
        _fuzzyWuzzy.Process_ExtractAll("goolge", (List<TextRecord>)null!, 0, out List<ResultRecord> results);
        Assert.That(results, Is.Empty);
    }

    [Test]
    public void Process_ExtractSorted_ReturnsResultsOrderedByScoreDescending()
    {
        List<TextRecord> strings = new List<TextRecord>
        {
            new TextRecord("google"),
            new TextRecord("bing"),
            new TextRecord("googleplus"),
        };

        _fuzzyWuzzy.Process_ExtractSorted("goolge", strings, 0, out List<ResultRecord> results);

        Assert.That(results[0].Score, Is.GreaterThanOrEqualTo(results[^1].Score));
    }

    [Test]
    public void Process_ExtractSorted_NullStrings_ReturnsEmptyListInsteadOfThrowing()
    {
        _fuzzyWuzzy.Process_ExtractSorted("goolge", (List<TextRecord>)null!, 0, out List<ResultRecord> results);
        Assert.That(results, Is.Empty);
    }
}
