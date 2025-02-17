import { AfterViewInit, Component, ElementRef, inject, viewChild } from '@angular/core';
import { HomeService } from '../home.service';

@Component({
  selector: 'app-home-parallax-svg',
  standalone: true,
  imports: [],
  templateUrl: './home-parallax-svg.component.html',
  styleUrl: './home-parallax-svg.component.css'
})
export class HomeParallaxSvgComponent implements AfterViewInit{
  homeService = inject(HomeService);

  layer1 = viewChild<ElementRef>("sky");
  layer2 = viewChild<ElementRef>("upperbck");
  layer3 = viewChild<ElementRef>("mount");
  layer4 = viewChild<ElementRef>("lowerbck");
  //container = viewChild<ElementRef>("container");


  ngAfterViewInit(): void {
    this.homeService.animateHomeParallax(
      this.layer1()?.nativeElement,
      this.layer2()?.nativeElement,
      this.layer3()?.nativeElement,
      this.layer4()?.nativeElement,
    );
  }

}
