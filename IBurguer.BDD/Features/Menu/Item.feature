Feature: Item

Cenários para gerenciamento de itens do Menu

Scenario: Include item in Menu
	Given that i'm authenticated
	And that I have an item
	And it is of the <category> category
	When the item is added to the Menu
	Then the item must be available when I query for the <category> category

	Examples: 
	| category   |
	| MainDish   |
	| SideDish   |
	| Drink      |
	| Dessert    |
