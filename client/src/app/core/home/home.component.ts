import { AfterViewInit, Component, ElementRef, inject, viewChild } from '@angular/core';
import { HomeService } from './home.service';
import { NonAuthHomeComponent } from "./non-auth-home/non-auth-home.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NonAuthHomeComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements AfterViewInit{
  private homeService = inject(HomeService);
  typeWriter = viewChild<ElementRef>('typewriter');
  cursor = viewChild<ElementRef>('cursor');

  ngAfterViewInit(): void {
      this.homeService.typeWriterAnimation(
        this.typeWriter()?.nativeElement,
      this.cursor()?.nativeElement);
  }
}
