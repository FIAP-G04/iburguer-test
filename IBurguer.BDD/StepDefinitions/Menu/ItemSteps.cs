using IBurguer.BDD.Infrastructure.Menu;
using IBurguer.BDD.Model.Menu;

namespace IBurguer.BDD.StepDefinitions.Menu
{
    [Binding]
    public class ItemSteps
    {
        private readonly IMenuService _menuService;

        private AddMenuItemRequest _itemRequest;
        private Guid _addedItemId;

        public ItemSteps(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [Given(@"that i'm authenticated")]
        public async Task GivenThatImAuthenticated()
        {
            await _menuService.Authenticate();
        }


        [Given(@"that I have an item")]
        public void GivenQuePossuoUmItem()
        {
            _itemRequest = new()
            {
                Name = "BDD Menu Item name",
                Description = "BDD Menu Item description",
                Price = 10.0M,
                PreparationTime = 10,
                ImagesUrl = new string[] { "https://image.url.com" }
            };
        }

        [Given(@"it is of the (.*) category")]
        public void GivenEleEDaCategoriaCategoria(string category)
        {
            _itemRequest.Category = category;
        }

        [When(@"the item is added to the Menu")]
        public async Task WhenAdicionoItemAoMenu()
        {
            var result = await _menuService.AddItem(_itemRequest);
            _addedItemId = result.Id;
        }

        [Then(@"the item must be available when I query for the (.*) category")]
        public async Task ThenOItemDeveEstarDisponivelNoMenuAoConsultarACategoriaCategoria(string category)
        {
            var result = await _menuService.GetItems(category);

            var addedItem = result.FirstOrDefault(x => x.Id == _addedItemId);
            addedItem.Should().NotBeNull();

            addedItem!.Category.Should().Be(category);
        }

    }
}
