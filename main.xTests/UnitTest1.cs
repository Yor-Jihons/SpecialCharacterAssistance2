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
    public void ReplacerTest()
    {
        // TODO: Test for Replacers.Replacer
    }
}