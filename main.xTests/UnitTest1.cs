using Xunit;

namespace main.xTests;

public class UnitTest1
{
    [Fact]
    public void SpecialCharacterTest()
    {
        var specialCharacter = new SpecialCharacterAssistance2.ClassMappings.SpecialCharacter(){
            Id = 1,
            CanUse = true,
            Character = "<",
            HtmlString = "&lt;",
            Description = "小なり"
        };

        Assert.Equal( 1, specialCharacter.Id );
        Assert.True( specialCharacter.CanUse );
        Assert.Equal( "<", specialCharacter.Character );
        Assert.Equal( "&lt;", specialCharacter.HtmlString );
        Assert.Equal( "小なり", specialCharacter.Description );
    }

    [Fact]
    public void GenreTest()
    {
        var genre = new SpecialCharacterAssistance2.ClassMappings.Genre(){
            Name = "半角",
            CanReplce = true,
            CanUse = false,
            SpecialCharacters = new System.Collections.Generic.List<SpecialCharacterAssistance2.ClassMappings.SpecialCharacter>(){
                new SpecialCharacterAssistance2.ClassMappings.SpecialCharacter(){
                    Id = 1,
                    CanUse = true,
                    Character = "<",
                    HtmlString = "&lt;",
                    Description = "小なり"
                },
            },
        };

        Assert.Equal( "半角", genre.Name );
        Assert.True( genre.CanReplce );
        Assert.False( genre.CanUse );
        Assert.Equal( 1, genre.SpecialCharacters.Count );
        Assert.Equal( "<", genre.SpecialCharacters[0].Character );
    }

    [Fact]
    public void SpecialCharactersTest()
    {
        var specialCharacters = new SpecialCharacterAssistance2.ClassMappings.SpecialCharacters(){
            Genres = new System.Collections.Generic.List<SpecialCharacterAssistance2.ClassMappings.Genre>(){
                new SpecialCharacterAssistance2.ClassMappings.Genre(){
                    Name = "半角",
                    CanReplce = true,
                    CanUse = false,
                    SpecialCharacters = new System.Collections.Generic.List<SpecialCharacterAssistance2.ClassMappings.SpecialCharacter>(){
                        new SpecialCharacterAssistance2.ClassMappings.SpecialCharacter(){
                            Id = 1,
                            CanUse = true,
                            Character = "<",
                            HtmlString = "&lt;",
                            Description = "小なり"
                        },
                    },
                },
                new SpecialCharacterAssistance2.ClassMappings.Genre(){
                    Name = "ロシア語",
                    CanReplce = true,
                    CanUse = false,
                    SpecialCharacters = new System.Collections.Generic.List<SpecialCharacterAssistance2.ClassMappings.SpecialCharacter>(){
                        new SpecialCharacterAssistance2.ClassMappings.SpecialCharacter(){
                            Id = 1,
                            CanUse = true,
                            Character = "д",
                            HtmlString = "",
                            Description = "ロシア語のD"
                        },
                    },
                }
            }
        };

        Assert.Equal( 2, specialCharacters.Genres.Count );
        Assert.Equal( "ロシア語", specialCharacters.Genres[1].Name );
    }

    [Fact]
    public void SpecialCharactersWithFileTest()
    {
        string filepath = @"..\..\..\specialcharacters.json";
        var specialCharacters = SpecialCharacterAssistance2.ClassMappings.SpecialCharacters.Load( filepath );
        Assert.Equal( 11, specialCharacters.Genres.Count );
        Assert.Equal( "ロシア語(大文字)", specialCharacters.Genres[1].Name );
    }

    [Fact]
    public void ReplacerTest1()
    {
        string target = "<html><body><p>This is test.</p></body></html>";

        string expected = "&lt;html&gt;&lt;body&gt;&lt;p&gt;This&nbsp;is&nbsp;test.&lt;/p&gt;&lt;/body&gt;&lt;/html&gt;";

        string filepath = @"..\..\..\specialcharacters.json";
        var specialCharacters = SpecialCharacterAssistance2.ClassMappings.SpecialCharacters.Load( filepath );

        var replacer = new SpecialCharacterAssistance2.Replacers.Replacer( target );
        Assert.Equal( target, replacer.TargetText );
        replacer.Begin();
        Assert.Equal( target, replacer.TargetText );
        replacer.Replace( specialCharacters );
        Assert.Equal( expected, replacer.TargetText );
        replacer.End();
        Assert.Equal( expected, replacer.TargetText );
    }

    [Fact]
    public void ReplacerTest2()
    {
        string target = "<html><body><p>This is test &amp; run.</p></body></html>";
        string exepcted1 = "<html><body><p>This is test AMPERSAND_UNREPLACEDamp; run.</p></body></html>";
        string expected2 = "&lt;html&gt;&lt;body&gt;&lt;p&gt;This&nbsp;is&nbsp;test&nbsp;AMPERSAND_UNREPLACEDamp;&nbsp;run.&lt;/p&gt;&lt;/body&gt;&lt;/html&gt;";
        string expected3 = "&lt;html&gt;&lt;body&gt;&lt;p&gt;This&nbsp;is&nbsp;test&nbsp;&amp;&nbsp;run.&lt;/p&gt;&lt;/body&gt;&lt;/html&gt;";

        string filepath = @"..\..\..\specialcharacters.json";
        var specialCharacters = SpecialCharacterAssistance2.ClassMappings.SpecialCharacters.Load( filepath );

        var replacer = new SpecialCharacterAssistance2.Replacers.Replacer( target );
        Assert.Equal( target, replacer.TargetText );
        replacer.Begin();
        Assert.Equal( exepcted1, replacer.TargetText );
        replacer.Replace( specialCharacters );
        Assert.Equal( expected2, replacer.TargetText );
        replacer.End();
        Assert.Equal( expected3, replacer.TargetText );
    }


    [Fact]
    public void ReplacerTest3()
    {
        string target = "&lt;html><body>&lt;p>This is test &amp; run.</p&gt;</body&gt;</html>";

        string expected1 = "AMPERSAND_UNREPLACEDlt;html><body>AMPERSAND_UNREPLACEDlt;p>This is test AMPERSAND_UNREPLACEDamp; run.</pAMPERSAND_UNREPLACEDgt;</bodyAMPERSAND_UNREPLACEDgt;</html>";
        string expected2 = "AMPERSAND_UNREPLACEDlt;html&gt;&lt;body&gt;AMPERSAND_UNREPLACEDlt;p&gt;This&nbsp;is&nbsp;test&nbsp;AMPERSAND_UNREPLACEDamp;&nbsp;run.&lt;/pAMPERSAND_UNREPLACEDgt;&lt;/bodyAMPERSAND_UNREPLACEDgt;&lt;/html&gt;";
        string expected3 = "&lt;html&gt;&lt;body&gt;&lt;p&gt;This&nbsp;is&nbsp;test&nbsp;&amp;&nbsp;run.&lt;/p&gt;&lt;/body&gt;&lt;/html&gt;";

        string filepath = @"..\..\..\specialcharacters.json";
        var specialCharacters = SpecialCharacterAssistance2.ClassMappings.SpecialCharacters.Load( filepath );

        var replacer = new SpecialCharacterAssistance2.Replacers.Replacer( target );
        Assert.Equal( target, replacer.TargetText );
        replacer.Begin();
        Assert.Equal( expected1, replacer.TargetText );
        replacer.Replace( specialCharacters );
        Assert.Equal( expected2, replacer.TargetText );
        replacer.End();
        Assert.Equal( expected3, replacer.TargetText );
    }
}