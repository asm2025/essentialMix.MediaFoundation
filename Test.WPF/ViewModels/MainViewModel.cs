﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using essentialMix;
using essentialMix.Core.WPF.Commands;
using essentialMix.Patterns.NotifyChange;
using JetBrains.Annotations;

namespace Test.WPF.ViewModels;

public class MainViewModel : NotifyPropertyChangedBase
{
	private ViewModelBase _selectedViewModel;

	public MainViewModel()
	{
		AppInfo appInfo = new AppInfo(typeof(MainViewModel).Assembly);
		Title = appInfo.Title;
		ViewModels = new ObservableCollection<ViewModelBase>
		{
			new ObservableDictionaryViewModel(),
			new ObservableHashSetViewModel(),
			new ObservableKeyedDictionaryViewModel(),
			new ObservableListViewModel(),
			new ObservableSortedSetViewModel()
		};
		ChangeViewCommand = new RelayCommand<ViewModelBase>((_, vm) => SelectedViewModel = vm, (_, vm) => vm != SelectedViewModel);
		GenerateCommand = new RelayCommand(_ => SelectedViewModel.Generate(), _ => SelectedViewModel != null);
		ClearCommand = new RelayCommand(_ => SelectedViewModel.Clear(), _ => SelectedViewModel != null);
	}

	[NotNull]
	public string Title { get; }

	public ViewModelBase SelectedViewModel
	{
		get => _selectedViewModel;
		set
		{
			if (_selectedViewModel == value) return;
			_selectedViewModel = value;
			OnPropertyChanged();
		}
	}

	[NotNull]
	public ObservableCollection<ViewModelBase> ViewModels { get; }

	[NotNull]
	public ICommand ChangeViewCommand { get; }

	[NotNull]
	public ICommand GenerateCommand { get; }

	[NotNull]
	public ICommand ClearCommand { get; }
}