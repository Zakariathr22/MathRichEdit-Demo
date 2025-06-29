using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MathRichEdit_Demo.Helpers;
using Microsoft.UI.Text;

namespace MathRichEdit_Demo;

public sealed partial class InitialPage : Page
{
    private List<MathSymbol> Symbols = MathModeHelper.GetSymbols();
    private List<MathStructure> BasicStructures;
    private List<MathStructure> Integrals;
    private List<MathStructure> LargeOperators;
    private List<MathStructure> Accents;
    private List<MathStructure> LimitAndFunctions;
    private List<MathStructure> Matrices;

    public InitialPage()
    {
        this.InitializeComponent();

        MathEditor.TextDocument.SetMathMode(RichEditMathMode.MathOnly);
        MathSymbolsItems.ItemsSource = Symbols;
    }

    private void SymbolDataTemplate_Loading(FrameworkElement sender, object args)
    {
        if (sender is Grid grid)
        {
            int index = MathSymbolsItems.GetElementIndex(grid);

            if (index % 2 == 0)
            {
                grid.Style = (Style)Resources["ItemStyle1"];
            }
            else
            {
                grid.Style = (Style)Resources["ItemStyle2"];
            }
        }
    }

    private void InsertSymbolBtn_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button insertionBtn)
        {
            if (MathEditor == null) return;

            MathEditor.Focus(FocusState.Programmatic);

            var selection = MathEditor.Document.Selection;
            if (selection != null)
            {
                InputSender.SendUnicodeText(insertionBtn.Tag.ToString());
            }
        }
    }

    private void SelectorBar_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
    {
        if (sender is SelectorBar selectorBar)
        {
            if (selectorBar.SelectedItem.Tag == null) return;

            if (selectorBar.SelectedItem.Tag.ToString() == "Symbols")
            {
                SymbolsTable.Visibility = Visibility.Visible;
                StructuresTable.Visibility = Visibility.Collapsed;
            }
            else
            {
                StructuresTable.Visibility = Visibility.Visible;
                SymbolsTable.Visibility = Visibility.Collapsed;
                switch (selectorBar.SelectedItem.Tag.ToString())
                {
                    case "Structures":
                        if (BasicStructures == null)
                        {
                            BasicStructures = MathModeHelper.GetStructures(StructuresCategory.BasicStructures);
                        }
                        MathStructuresItems.ItemsSource = BasicStructures;
                        break;
                    case "Integrals":
                        if (Integrals == null)
                        {
                            Integrals = MathModeHelper.GetStructures(StructuresCategory.Integrals);
                        }
                        MathStructuresItems.ItemsSource = Integrals;
                        break;
                    case "LargeOperators":
                        if (LargeOperators == null)
                        {
                            LargeOperators = MathModeHelper.GetStructures(StructuresCategory.LargeOperators);
                        }
                        MathStructuresItems.ItemsSource = LargeOperators;
                        break;
                    case "Accents":
                        if (Accents == null)
                        {
                            Accents = MathModeHelper.GetStructures(StructuresCategory.Accents);
                        }
                        MathStructuresItems.ItemsSource = Accents;
                        break;
                    case "LimitAndFunctions":
                        if (LimitAndFunctions == null)
                        {
                            LimitAndFunctions = MathModeHelper.GetStructures(StructuresCategory.LimitAndFunctions);
                        }
                        MathStructuresItems.ItemsSource = LimitAndFunctions;
                        break;
                    case "Matrices":
                        if (Matrices == null)
                        {
                            Matrices = MathModeHelper.GetStructures(StructuresCategory.Matrices);
                        }
                        MathStructuresItems.ItemsSource = Matrices;
                        break;
                }
            }
        }
    }

    private void StructureDataTemplate_Loading(FrameworkElement sender, object args)
    {
        if (sender is Grid grid)
        {
            int index = MathStructuresItems.GetElementIndex(grid);

            if (index % 2 == 0)
            {
                grid.Style = (Style)Resources["ItemStyle1"];
            }
            else
            {
                grid.Style = (Style)Resources["ItemStyle2"];
            }
        }
    }

    private void LightThemeImage_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is Image image)
        {
            if (image.ActualTheme == ElementTheme.Light)
            {
                image.Visibility = Visibility.Visible;
            }
            else
            {
                image.Visibility = Visibility.Collapsed;
            }
        }
    }

    private void DarkThemeImage_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is Image image)
        {
            if (image.ActualTheme == ElementTheme.Dark)
            {
                image.Visibility = Visibility.Visible;
            }
            else
            {
                image.Visibility = Visibility.Collapsed;
            }
        }
    }

    private void LightThemeImage_ActualThemeChanged(FrameworkElement sender, object args)
    {
        if (sender is Image image)
        {
            if (image.ActualTheme == ElementTheme.Light)
            {
                image.Visibility = Visibility.Visible;
            }
            else
            {
                image.Visibility = Visibility.Collapsed;
            }
        }
    }

    private void DarkThemeImage_ActualThemeChanged(FrameworkElement sender, object args)
    {
        if (sender is Image image)
        {
            if (image.ActualTheme == ElementTheme.Dark)
            {
                image.Visibility = Visibility.Visible;
            }
            else
            {
                image.Visibility = Visibility.Collapsed;
            }
        }
    }

    private void InsertStructureBtn_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button insertionBtn)
        {
            if (MathEditor == null) return;

            MathEditor.Focus(FocusState.Programmatic);

            var selection = MathEditor.Document.Selection;
            if (selection != null)
            {
                InputSender.SendUnicodeText(insertionBtn.Tag.ToString());
            }
        }
    }
}
