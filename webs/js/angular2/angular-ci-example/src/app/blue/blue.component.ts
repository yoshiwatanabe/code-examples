import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-blue',
  template: `
    <div class="wrapper"></div>
  `,
  styles: ['div.wrapper { width: 500px; height: 500px; background-color: blue; }']
})
export class BlueComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
