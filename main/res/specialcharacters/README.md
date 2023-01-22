# specialcharacters.json

This is a json file which I write some special characters for some app of mine.

## Usage Flow

### Step 1. Put this directory to your project.

Put this directory to your project which wants to use this.

### Step 2. Link this to your project.

If you use JavaScript, just write them like ``sc.Genre[0].Name``.
If you use oher programming languages, you should adapt them.

## Usage

### Structures

```
OBJECT_NAME
    - Genre[0]
        - Name
        - CanReplce
        - CanUse
        - SpecialCharacters[0]
            - Id
            - CanUse
            - Character
            - HtmlString
            - Description
        - SpecialCharacters[1]
            - ...
        - ...

    - Genre[1]
        - ...
```

### Sample

```JavaScript
var test = { ... }; // the variable test has the ojbect of this json.

// Genres[i].Name is the string as type name like "Double-byte symbol".
console.log("test.Genre[0].Name = " + test.Genre[0].Name);
// Genres[i].CanRepace is whether you can replace the target to this character or not. (You can change this value.) This should be a boolean value.
console.log("test.Genre[0].CanReplce = " + test.Genre[0].CanReplce);
// Genres[i].CanRepace is whether this Genres[i] can be used or not. (You can change this value.) This should be a boolean value.
console.log("test.Genre[0].CanUse = " + test.Genre[0].CanUse);

// Genre[i].SpecialCharacters[j].Id is the ID of the character. This should be a number.
var strIDs = "{";
for (var i = 0; i < test.Genre[0].SpecialCharacters.length; i++) {
    strIDs += test.Genre[0].SpecialCharacters[i].Id;
    if (i !== test.Genre[0].SpecialCharacters.length - 1) {
        strIDs += ",";
    }
}
strIDs += "}";
console.log("IDs of test.Genre[0].SpecialCharacters = " + strIDs);

// Genre[i].SpecialCharacters[j].CanUse is you can use this character. (You can change this value.) This should be a boolean value.
var strCanUses = "{";
for (var i = 0; i < test.Genre[0].SpecialCharacters.length; i++) {
    strCanUses += test.Genre[0].SpecialCharacters[i].CanUse;
    if (i !== test.Genre[0].SpecialCharacters.length - 1) {
        strCanUses += ",";
    }
}
strCanUses += "}";
console.log("CanUses of test.Genre[0].SpecialCharacters = " + strCanUses);

// Genre[i].SpecialCharacters[j].Character is the the specail character like "Д".
var strCharacters = "{";
for (var i = 0; i < test.Genre[0].SpecialCharacters.length; i++) {
    strCharacters += '"' + test.Genre[0].SpecialCharacters[i].Character + '"';
    if (i !== test.Genre[0].SpecialCharacters.length - 1) {
        strCharacters += ",";
    }
}
strCharacters += "}";
console.log("Characters of test.Genre[0].SpecialCharacters = " + strCharacters);

// Genre[i].SpecialCharacters[j].HtmlString is the html string standing for the the specail character.
var strHtmlString = "{";
for (var i = 0; i < test.Genre[0].SpecialCharacters.length; i++) {
    strHtmlString += test.Genre[0].SpecialCharacters[i].HtmlString;
    if (i !== test.Genre[0].SpecialCharacters.length - 1) {
        strHtmlString += ",";
    }
}
strHtmlString += "}";
console.log("HTML of test.Genre[0].SpecialCharacters = " + strHtmlString);


// Genre[i].SpecialCharacters[j].Description is the description for the special character.
var strDescriptions = "{";
for (var i = 0; i < test.Genre[0].SpecialCharacters.length; i++) {
    strDescriptions += test.Genre[0].SpecialCharacters[i].Description;
    if (i !== test.Genre[0].SpecialCharacters.length - 1) {
        strDescriptions += ",";
    }
}
strDescriptions += "}";
console.log("Descriptions of test.Genre[0].SpecialCharacters = " + strDescriptions);
```

## License

This library is released under the MIT License. See also [LICENCE which included](./LICENSE) or [on GitHub](https://github.com/Yor-Jihons/specialcharacters/blob/main/LICENSE).

## References

- [html の特殊文字](https://www.cis.twcu.ac.jp/~asakawa/comp2d-2008/special_chars.html)
- [ロシア語アルファベット一覧](http://www.j-pca.net/world/language/russian/alpha.html)
- [ローマ字→キリル文字変換プログラム](https://rosianotomo.com/romcyr/romcyr.htm)

## Contact

Author: Yor-Jihons  
GitHub: [specialcharacters](https://github.com/Yor-Jihons/specialcharacters)  
