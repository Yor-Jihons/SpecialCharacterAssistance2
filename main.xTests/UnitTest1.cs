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

        Assert.Equal( specialCharacter.Id, 1 );
        Assert.True( specialCharacter.CanUse );
        Assert.Equal( specialCharacter.Character, "<" );
        Assert.Equal( specialCharacter.HtmlString, "&lt;" );
        Assert.Equal( specialCharacter.Description, "小なり" );
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

        Assert.Equal( genre.Name, "半角" );
        Assert.True( genre.CanReplce );
        Assert.False( genre.CanUse );
        Assert.Equal( genre.SpecialCharacters.Count, 1 );
        Assert.Equal( genre.SpecialCharacters[0].Character, "<" );
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

        Assert.Equal( specialCharacters.Genres.Count, 2 );
        Assert.Equal( specialCharacters.Genres[1].Name, "ロシア語" );
    }

    [Fact]
    public void ReplacerTest()
    {
        // TODO: Test for Replacers.Replacer
    }
}