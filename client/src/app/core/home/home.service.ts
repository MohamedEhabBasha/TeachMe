import { Injectable } from '@angular/core';
import { gsap } from 'gsap';
import { TextPlugin } from 'gsap/TextPlugin';
import { ScrollTrigger } from 'gsap/ScrollTrigger';
//import MotionPathPlugin from 'gsap/MotionPathPlugin';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor() {

    gsap.registerPlugin(TextPlugin);
    gsap.registerPlugin(ScrollTrigger);
    //gsap.registerPlugin(MotionPathPlugin);
    this.mainTimeline = gsap.timeline({
      repeat: -1,
      repeatDelay: 0
    });
    this.startTimeline = gsap.timeline();
  }
  public mainTimeline: gsap.core.Timeline;
  public startTimeline: gsap.core.Timeline;
  private sentence: string[] = ['Empowering Learning, One Step at a Time!!'];

  public typeWriterAnimation(typeElement: HTMLElement, cursorElement: HTMLElement) {
    this.sentence.forEach(sentence => {
      let textTimeLine = gsap.timeline({
        repeat: 1,
        yoyo: true,
        repeatDelay: 3
      });

      textTimeLine.to(typeElement, {
        text: sentence,
        duration: 3,
        onUpdate: () => {
          cursorTimeLine.restart();
          cursorTimeLine.pause();
        },
        onComplete: () => {
          cursorTimeLine.play();
        }
      });

      this.mainTimeline.add(textTimeLine);
    })

    let cursorTimeLine = gsap.timeline({
      repeat: -1,
      repeatDelay: .5
    })

    cursorTimeLine.to(cursorElement, {
      opacity: 1,
      duration: 0
    }).to(cursorElement, {
      opacity: 0,
      duration: 0,
      delay: .5
    })
  }

  public aboutAnimation(aboutSection: HTMLElement, aboutText: HTMLElement, aboutImage: HTMLElement, aboutCard: HTMLElement) {
    let start = "top 50%";
    let end = "top 20%";
    gsap.from([aboutText, aboutImage], {
          scrollTrigger: {
              trigger: aboutSection,
              start: start,
              end: end,
              scrub: 0.5
          },
          x: -100,
          opacity: 0,
          duration: 2,
          stagger: 0.2
      });

      gsap.from(aboutCard, {
          scrollTrigger: {
              trigger: aboutSection,
              start: start,
              end: end,
              scrub: 0.5
          },
          x: 100,
          opacity: 0,
          duration: 2,
          stagger: 0.2
      });
  }
  public info1Animation(infoSection: HTMLElement, infoLetter: HTMLElement) {
    let start = "top 50%";
    let end = "top 20%";

    gsap.from(infoLetter, {
      scrollTrigger: {
          trigger: infoSection,
          start: start,
          end: end,
          scrub: 0.5
      },
      xPercent: 100,
      opacity: 0,
      duration: 2,
      stagger: 0.2
    });
  }
  public animateGetStatredSection(mainSection: HTMLElement, left: HTMLElement, avatar: HTMLElement, right: HTMLElement) {
    this.startTimeline
    .from(left, { xPercent: -100, opacity: 0 , ease: "power2.out" })
    .from(avatar, { scale: 0, opacity: 0, duration: 5 })
    .from(right, { xPercent: 100,  ease: "power2.out" });

    ScrollTrigger.create({
      animation: this.startTimeline,
      trigger: mainSection,
      start: "top center",
      end: "bottom bottom",
      scrub: 1,
      //snap: 1,
    })
  }
}
