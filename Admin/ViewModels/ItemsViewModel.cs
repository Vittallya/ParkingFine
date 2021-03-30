using MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DAL;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using Admin.Services;
using System.Data.Entity;
using Admin.Components;

namespace Admin.ViewModels
{
    public abstract class ItemsViewModel<T> : BasePageViewModel, IItemsViewModel where T: class, new()
    {
        protected readonly PageService pageservice;
        protected readonly AllDbContext dbContext;
        protected readonly FieldsGenerator fieldsGenerator;

        public virtual async Task<bool> CheckBeforeAdd(T item)
        {
            return true;
        }

        protected ItemsViewModel(PageService pageservice, AllDbContext dbContext, Services.FieldsGenerator fieldsGenerator) : base(pageservice)
        {
            this.pageservice = pageservice;
            this.dbContext = dbContext;
            this.fieldsGenerator = fieldsGenerator;
            Init();

            
        }

        async void Init()
        {
            GenerateBindingVIew();

            if (fieldsGenerator.IsLastDetail)
            {
                await FromDetailsPage(fieldsGenerator.IsEdit, fieldsGenerator.Item as T);
                fieldsGenerator.Clear();
            }
            await LoadItemsNew();

        }

        private void GenerateBindingVIew()
        {
            var list = BindingList1.
                Where(x => x.PropertyType != PropertyType.OuterPropertyIdNonVisible).
                ToList();

            GridView.Columns.Clear();
            foreach (var bind in list)
            {
                GridViewColumn item = new GridViewColumn();

                item.DisplayMemberBinding = new Binding(bind.BindingPath);
                item.Header = bind.DisplayName;
                GridView.Columns.Add(item);
            }
        }

        public override int PoolIndex { get; } = 1;

        public T SelectedItem { get; set; }
        public ObservableCollection<T> Items { get; set; }

        public async Task Remove(T item)
        {
            dbContext.Set<T>().Remove(item);
            await dbContext.SaveChangesAsync();
        }
        public async Task Add(T item)
        {
            dbContext.Set<T>().Add(item);
            await dbContext.SaveChangesAsync();
        }


        public abstract Task Edit(T item);

        public ICommand RemoveCommand => new CommandAsync( async x =>
        {
            await Remove(SelectedItem);
            await LoadItemsNew();

        }, x => SelectedItem != null);

        public ICommand EditCommand => new Command(x =>
        {            
            ToDetailsPage(true);
        }, x => SelectedItem != null);

        public ICommand AddCommand => new Command(x =>
        {            
            ToDetailsPage(false);
        });
        public ICommand UpdateCommand => new CommandAsync(async x =>
        {
            await LoadItemsNew();

        });

        async Task LoadItemsNew()
        {
            await dbContext.Set<T>().LoadAsync();
            Items = new ObservableCollection<T>(dbContext.Set<T>());
        }

        async Task FromDetailsPage(bool isEdit, T item)
        {
            if (isEdit)
            {
                await Edit(item);
            }
            else if(await CheckBeforeAdd(item))
            {                
                await Add(item);
            }
        }

        async void ToDetailsPage(bool isEdit)
        {
            T item = isEdit ? SelectedItem : new T();

            await fieldsGenerator.Generate(item, BindingList1, isEdit);
            Locator.SetDetailsViewModel<T>();
            pageservice.ChangePage<Pages.ItemDetailPage>(PoolIndex, DisappearAndToSlideAnim.ToLeft);
        }

        public abstract Dictionary<string, string> BindingList { get; }

        public GridView GridView { get; set; } = new GridView();

        public StackPanel StackPanel { get; set; } = new StackPanel();

        public abstract BindingComponent[] BindingList1 { get; }
    }
}
