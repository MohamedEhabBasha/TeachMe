import { AfterViewInit, Component, ElementRef, inject, viewChild } from '@angular/core';
import { HomeService } from './home.service';
import { NonAuthHomeComponent } from "./non-auth-home/non-auth-home.component";
import { HomeParallaxSvgComponent } from "./home-parallax-svg/home-parallax-svg.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NonAuthHomeComponent, HomeParallaxSvgComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements AfterViewInit{
  private homeService = inject(HomeService);
  typeWriter = viewChild<ElementRef>('typewriter');
  cursor = viewChild<ElementRef>('cursor');
  main = viewChild<ElementRef>('main');
  homeImg = viewChild<ElementRef>('homeImg');

  ngAfterViewInit(): void {
     this.homeService.main.set(this.main()?.nativeElement);

      this.homeService.typeWriterAnimation(
        this.typeWriter()?.nativeElement,
      this.cursor()?.nativeElement);

    this.homeService.animateHomeImg(this.homeImg()?.nativeElement);
  }
}
