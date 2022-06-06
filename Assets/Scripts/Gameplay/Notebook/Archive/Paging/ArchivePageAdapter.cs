using UnityEngine;
using UnityEngine.UI;
using ProjectWendigo.Database.Extensions.Reference;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewArchivePageAdapter", menuName = "Notebook/Archive/Page Adapter")]
    public class ArchivePageAdapter : INotebookPageAdapter
    {
        private ArchiveCategoryEntry GetCategory(ArchiveCollectionEntry entry, ArchivePageAdapterOptions options)
        {
            ArchiveCategoryEntry collectionEntry = entry.ResolveReference<ArchiveCategoryEntry>(new ReferenceToOne
            {
                KeyName = "CategoryTitle",
                ForeignKeyName = "Title",
                ForeignTable = options.Categories
            });
            Debug.Assert(collectionEntry != null, $"No category entry found matching given title");
            return collectionEntry;
        }

        private Transform GetCategoryTab(GameObject page, ArchiveCategoryEntry category)
        {
            return page.transform
                .Find($"Entries/{category.Id} {category.Title}", true);
        }

        private Transform GetActiveCategoryTab(GameObject page)
        {
            Transform[] transforms = page.transform.Find("Entries").GetComponentsInChildren<Transform>(false);
            // First transform is Entries's, others are its children's
            return transforms.Length > 1 ? transforms[1] : null;
        }

        private void SetActiveCategoryTab(GameObject page, ArchiveCategoryEntry category)
        {
            this.GetActiveCategoryTab(page)?.gameObject.SetActive(false);
            this.GetCategoryTab(page, category).gameObject.SetActive(true);
        }

        private GameObject CreateCategoryButton(GameObject page, ArchiveCategoryEntry category, ArchivePageAdapterOptions options)
        {
            var categoriesTransform = page.transform.Find("Categories/View/Entries");
            var categoryButtonGameObject = options.CategoryAdapter.CreateElement(category, options.CategoryAdapterOptions);
            categoryButtonGameObject.transform.SetParent(categoriesTransform, false);
            categoryButtonGameObject.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                this.SetActiveCategoryTab(page, category);
            });
            return categoryButtonGameObject;
        }

        private GameObject CreateCategoryTab(GameObject page, ArchiveCategoryEntry category, ArchivePageAdapterOptions options)
        {
            var entriesTransform = page.transform.Find("Entries");
            var categoryTabGameObject = Instantiate(options.CategoryTabPrefab, entriesTransform);
            categoryTabGameObject.name = $"{category.Id} {category.Title}";
            return categoryTabGameObject;
        }

        private Transform GetCategoryEntriesView(GameObject page, ArchiveCategoryEntry category)
        {
            return this.GetCategoryTab(page, category)
                .Find("View/Entries");
        }

        public override GameObject CreatePage(INotebookPageAdapterOptions options)
        {
            var adapterOptions = (ArchivePageAdapterOptions)options;
            var page = Instantiate(adapterOptions.PagePrefab);
            foreach (ArchiveCategoryEntry category in adapterOptions.Categories.GetAll())
            {
                var categoryTab = this.CreateCategoryTab(page, category, adapterOptions);
                categoryTab.SetActive(false);
            }
            return page;
        }

        public override void AddChild(GameObject page, GameObject child, object entry, INotebookPageAdapterOptions options)
        {
            var archiveEntry = (ArchiveCollectionEntry)entry;
            var adapterOptions = (ArchivePageAdapterOptions)options;
            var category = this.GetCategory(archiveEntry, adapterOptions);
            var categoryEntriesView = this.GetCategoryEntriesView(page, category);
            if (categoryEntriesView.childCount == 0)
            {
                // We're adding the first child of this category => add category button
                this.CreateCategoryButton(page, category, adapterOptions);
            }
            if (this.GetActiveCategoryTab(page) == null)
            {
                // No category tab is active so far, then activate the current one
                this.SetActiveCategoryTab(page, category);
            }
            child.transform.SetParent(categoryEntriesView, false);
        }
    }
}