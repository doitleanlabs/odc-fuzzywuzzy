using NUnit.Framework;
using OutSystems.ExternalLibraries.SDK;

namespace DoiTLean.FuzzyWuzzy.UnitTests;

public class FuzzyWuzzyTests {


    [Test]
    public void FuzzyWuzzyTest() {
        Assert.That("2", Is.EqualTo("2"));
    }


}