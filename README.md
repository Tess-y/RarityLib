# RarityLib

Adds the ability to create and fetch custom rarities for cards, along with functionality for modifying the relative rarity of a card.

Currently there are 3 known additional rarities for cards:
- Legendary (from this mod)
- Epic (WillsWackyManagers)
- Scarce (WillsWackyManagers)
- <sub>If you made a rarity you want added to this list, please DM 『 root 』#9063 on discord. If you do not share a server with us, you can use this link: https://discord.gg/9XWgYUUEzZ</sub>

<details>
<summary>Functions</summary>
### AddRarity()
```cs
int AddRarity(string name, float relativeRarity, Color color, Color colorOff)
```
#### Description
Adds a new rarity for cards to utilize and returns the integer representation of that value.

#### Parameters
- *string* `name` the name of the rarity to add.
- *float* `relativeRarity` how rare should it be (Common is 1, Rare is 0.1).
- *Color* `color` what color should the rarity be when the card is selected.
- *Color* `colorOff` what color should the rarity be when the card is not selected.

#### Example Usage
```CSHARP
RarityUtils.AddRarity("Legendary", 0.025f, new Color(1, 1, 0), new Color(0.7f, 0.7f, 0));
```

#### Notes
AddRarity should be utilized in your mod's Awake function, that way the rarities are present before building the cards that need them.

### GetRarity()
```cs
CardInfo.Rarity GetRarity(string rarityName)
```
#### Description
Returns the rarity with the given name if it exists, otherwise returns Common.

#### Parameters
- *string* `rarityName` the rarity to fetch.

#### Example Usage
```CSHARP
RarityUtils.GetRarity("Legendary");
```

### GetRarityData()
```cs
Rarity GetRarityData(CardInfo.Rarity rarity)
```
#### Description
Returns the data for a rarity.

#### Parameters
- *CardInfo.Rarity* `rarity` the rarity data to fetch.

#### Example Usage
```CSHARP
RarityUtils.GetRarityData(CardInfo.Rarity.Common);
```

### GetCardRarityModifier()
```cs
float GetCardRarityModifier(CardInfo card)
```
#### Description
Returns the rarity modifier for a card.

#### Parameters
- *CardInfo* `card` the rarity data to fetch.

#### Example Usage
```CSHARP
float rarityMod = RarityUtils.GetCardRarityModifier(card);
UnityEngine.Debug.Log(rarityMod);
```

### SetCardRarityModifier()
```cs
void SetCardRarityModifier(CardInfo card, float modifier)
```
#### Description
Sets the default rarity modifier for a card.

#### Parameters
- *CardInfo* `card` the rarity data to fetch.
- *float* `modifier` the new default rarity modifier for the card.

#### Example Usage
```CSHARP
RarityUtils.SetCardRarityModifier(card, 5);
```

### AjustCardRarityModifier()
```cs
void AjustCardRarityModifier(CardInfo card, float add = 0, float mul = 0)
```
#### Description
Adjusts the rarity modifier for a card.

#### Parameters
- *CardInfo* `card` the rarity data to fetch.
- *float* `add` the amount the default rarity modifier is adjusted by additively.
- *float* `mul` the amount the default rarity modifier is adjusted by multiplicatively. Applies after the additive modifier.

#### Example Usage
```CSHARP
RarityUtils.AjustCardRarityModifier(CardInfo card, 5, 100)
```
</details>

<details>
<summary>Classes</summary>

### Rarity

#### Description

The information about a given card rarity.

#### Fields

- string name
  - The name of a rarity.
- float relativeRarity
  - How common the rarity is compared to commons.
- float calculatedRarity
  - No idea.
- Color color
  - The color of the rarity when a card is selected.
- Color colorOff
  - The color of the rarity when a card is not selected.
- CardInfo.Rarity value
  - The CardInfo rarity of a card rarity.
</details>
