using Admin.Services;
using Admin.ViewModels.Interfaces;
using MVVM_Core;
using System.Windows.Controls;
using System.Windows.Input;

namespace Admin.ViewModels
{
    public class EditItemViewModel<T> : BasePageViewModel, IEditItem where T: class
    {
        private readonly PageService pageservice;
        private readonly FieldsGenerator fieldsGenerator;

        public EditItemViewModel(PageService pageservice, Services.FieldsGenerator fieldsGenerator) : base(pageservice)
        {
            this.pageservice = pageservice;
            this.fieldsGenerator = fieldsGenerator;
            SelectedItem = fieldsGenerator.Item as T;
            Content.DataContext = SelectedItem;

            Content.Children.Clear();
            fieldsGenerator.GetGenerated(Content);
        }

        public T SelectedItem { get; set; }

        public ICommand Accept => new Command(x =>
        {            
            
            fieldsGenerator.SetItem(SelectedItem);
            pageservice.ChangePage<Pages.ItemsPage>(DisappearAndToSlideAnim.ToRight);
        });

        public ICommand Cancel => new Command(x =>
        {
            pageservice.Back<Pages.ItemsPage>(PoolIndex, DisappearAndToSlideAnim.ToRight);
            
        });


        public override int PoolIndex => 1;

        public StackPanel Content { get; set; } = new StackPanel();
    }
}