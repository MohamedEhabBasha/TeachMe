import { Component, input, OnInit, output } from '@angular/core';

@Component({
  selector: 'app-category-selector',
  standalone: true,
  imports: [],
  templateUrl: './category-selector.component.html',
  styleUrl: './category-selector.component.css'
})
export class CategorySelectorComponent implements OnInit{
  availableCategories: string[] = [
    'Science', 'Technology', 'Sports', 'Health', 'Music', 'Art', 'Education'
  ];

  currentSelectedCategories = input<string[]>([]);
  newCategories = output<string[]>();
  selectedCategories: string[] = [];

  ngOnInit(): void {
      this.selectedCategories = this.currentSelectedCategories();
  }

  onCategoryChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    const selectedOption = Array.from(selectElement.selectedOptions).map(option => option.value);
    //console.log(selectedOption);
    if (this.selectedCategories.length > 2) {
      alert('You can select up to 3 categories only.');
      // Restore the previous valid state
      selectElement.value = '';
      return;
    }

    this.selectedCategories.push(selectedOption[0]);
    this.newCategories.emit(this.selectedCategories);
  }

  removeCategory(category: string) {
    this.selectedCategories = this.selectedCategories.filter(c => c !== category);
    this.newCategories.emit(this.selectedCategories);
  }
}
