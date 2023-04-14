namespace Routers.Tests;

public class GraphTests
{
    private static bool AreFilesEqual(string firstFilePath, string secondFilePath)
    {
        using var firstFileStream = File.Open(firstFilePath, FileMode.Open);
        using var secondFileStream = File.Open(secondFilePath, FileMode.Open);

        var firstFileByte = firstFileStream.ReadByte();
        var secondFileByte = secondFileStream.ReadByte();

        while (firstFileByte != -1)
        {
            if (firstFileByte != secondFileByte)
            {
                return false;
            }

            firstFileByte = firstFileStream.ReadByte();
            secondFileByte = secondFileStream.ReadByte();
        }

        return firstFileByte == secondFileByte;
    }
    
    [Test]
    public void PrintIntoNullPathShouldThrowArgumentNullException()
    {
        var graph = new Graph();
        Assert.Throws<ArgumentNullException>(() => graph.Print(null));
    }

    [Test]
    public void ParseFromNullPathShouldThrowArgumentNullException()
    {
        var graph = new Graph();
        Assert.Throws<ArgumentNullException>(() => graph.Parse(null));
    }

    [Test]
    public void ParseEmptyFileShouldThrowParseException()
    {
        var graph = new Graph();
        Assert.Throws<ParseException>(() => graph.Parse(PathToParseTests + "Parse_emptyFile.txt"));
    }
    
    [Test]
    public void ParseFirstEmptyLineShouldThrowParseException()
    {
        var graph = new Graph();
        Assert.Throws<ParseException>(() => graph.Parse(PathToParseTests + "Parse_firstEmptyLine.txt"));
    }
    
    [Test]
    public void ParseLastEmptyLineShouldNotThrowException()
    {
        var graph = new Graph();
        Assert.DoesNotThrow(() => graph.Parse(PathToParseTests + "Parse_lastEmptyLine.txt"));
    }
    
    [Test]
    public void NodeWithoutEdgesShouldThrowParseException()
    {
        var graph = new Graph();
        Assert.Throws<ParseException>(() => graph.Parse(PathToParseTests + "Parse_nodeWithoutEdges.txt"));
    }
    
    [Test]
    public void SeparateWrongCharsShouldThrowParseException()
    {
        var graph = new Graph();
        Assert.Throws<ParseException>(() => graph.Parse(PathToParseTests + "Parse_separateWrongChars.txt"));
    }
    
    [Test]
    public void WrongCharWithNumbersShouldThrowParseException()
    {
        var graph = new Graph();
        Assert.Throws<ParseException>(() => graph.Parse(PathToParseTests + "Parse_wrongCharsNearNumbers.txt"));
    }
    
    [Test]
    public void WrongNumberOfNumbersOnLineShouldThrowParseException()
    {
        var graph = new Graph();
        Assert.Throws<ParseException>(() => graph.Parse(PathToParseTests + "Parse_wrongNumberOfNumbersOnLine.txt"));
    }
    
    [Test]
    public void SimpleParsePrintChecking()
    {
        var graph = new Graph();
        graph.Parse(PathToParseTests + "simpleInput.txt");
        
        using (var _ = File.Open(PathToParseTests + "simpleOutput.txt", FileMode.Create)) { }
        graph.Print(PathToParseTests + "simpleOutput.txt");
        
        Assert.That(AreFilesEqual(PathToParseTests + "simpleOutput.txt", PathToParseTests + "simpleInput.txt"), Is.True);
        
        File.Delete(PathToParseTests + "simpleOutput.txt");
    }
    
    [Test]
    public void ComplicatedParsePrintChecking()
    {
        var graph = new Graph();
        graph.Parse(PathToParseTests + "complicatedInput.txt");
        
        using (var _ = File.Open(PathToParseTests + "complicatedOutput.txt", FileMode.Create)) { }
        graph.Print(PathToParseTests + "complicatedOutput.txt");
        
        Assert.That(AreFilesEqual(PathToParseTests + "complicatedOutput.txt", PathToParseTests + "complicatedInput.txt"), Is.True);
        
        File.Delete(PathToParseTests + "complicatedOutput.txt");
    }
    
    [Test]
    public void SimpleParseTransformPrintChecking()
    {
        var graph = new Graph();
        graph.Parse(PathToParseTests + "simpleInput.txt");
        
        using (var _ = File.Open(PathToParseTests + "simpleOutput.txt", FileMode.Create)) { }
        graph.TransformToMaximalWeightTree();
        graph.Print(PathToParseTests + "simpleOutput.txt");
        
        Assert.That(AreFilesEqual(PathToParseTests + "simpleOutput.txt", PathToParseTests + "simpleExpectedOutput.txt"), Is.True);
        
        File.Delete(PathToParseTests + "simpleOutput.txt");
    }
    
    [Test]
    public void ComplicatedParseTransformPrintChecking()
    {
        var graph = new Graph();
        graph.Parse(PathToParseTests + "complicatedInput.txt");
        
        using (var _ = File.Open(PathToParseTests + "complicatedOutput.txt", FileMode.Create)) { }
        graph.TransformToMaximalWeightTree();
        graph.Print(PathToParseTests + "complicatedOutput.txt");
        
        Assert.That(AreFilesEqual(PathToParseTests + "complicatedOutput.txt", PathToParseTests + "complicatedExpectedOutput.txt"), Is.True);
        
        File.Delete(PathToParseTests + "complicatedOutput.txt");
    }
    
    private const string PathToParseTests = "../../../tests/Parse/";
    private const string PathToTransformTests = "../../../tests/Transform/";
}