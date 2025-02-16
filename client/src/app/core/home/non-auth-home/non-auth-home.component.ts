import { AfterViewInit, Component, ElementRef, inject, viewChild } from '@angular/core';
import { HomeService } from '../home.service';
import { SvgAvatarComponent } from "./svg-avatar/svg-avatar.component";
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { SvgBoyWavingAvatarComponent } from "./svg-boy-waving-avatar/svg-boy-waving-avatar.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-non-auth-home',
  standalone: true,
  imports: [SvgAvatarComponent, CarouselModule, SvgBoyWavingAvatarComponent],
  templateUrl: './non-auth-home.component.html',
  styleUrl: './non-auth-home.component.css'
})
export class NonAuthHomeComponent implements AfterViewInit{
  private homeService = inject(HomeService);
  private router = inject(Router)
    
  aboutText = viewChild<ElementRef>('aboutText');
  aboutImage = viewChild<ElementRef>('aboutImage');
  aboutCard = viewChild<ElementRef>('aboutCard');
  aboutSection = viewChild<ElementRef>('aboutSection');

  info1Section = viewChild<ElementRef>('info1Section');
  info1Letter = viewChild<ElementRef>('infoLetter');

  startSection = viewChild<ElementRef>("startSection");
  left = viewChild<ElementRef>("left");
  avatar = viewChild<ElementRef>("avatar");
  right = viewChild<ElementRef>("right");

  ngAfterViewInit(): void {
      this.homeService.aboutAnimation(
        this.aboutSection()?.nativeElement,
        this.aboutText()?.nativeElement,
        this.aboutImage()?.nativeElement,
        this.aboutCard()?.nativeElement
      );

      this.homeService.info1Animation(
        this.info1Section()?.nativeElement,
        this.info1Letter()?.nativeElement
      )

      this.homeService.animateGetStatredSection(
        this.startSection()?.nativeElement,
        this.left()?.nativeElement,
        this.avatar()?.nativeElement,
        this.right()?.nativeElement
      );
  }
  
  getStatred() {
    this.router.navigateByUrl('/register');
  }
}
