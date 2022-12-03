import {Component, OnInit} from '@angular/core';
import {customAxios, HttpService} from "../services/http.service";
import {Router, Event, NavigationStart, NavigationEnd, NavigationError, ActivatedRoute} from '@angular/router';
import {MatSnackBar} from "@angular/material/snack-bar";
import jwtDecode from "jwt-decode";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  isMenuOpen = false;
  toggleMenu():void{
    this.isMenuOpen = !this.isMenuOpen;
  }

  currentRoute: string = "";

  constructor(private router: Router,
              public http: HttpService,
              private snackBar: MatSnackBar) {
    this.router.events.subscribe((event: Event) => {
      if (event instanceof NavigationEnd) {
        this.currentRoute = event.url;
      }
    });
    let t = localStorage.getItem('token')
    if (t) {
      let decoded = jwtDecode(t) as any;
      this.http.userName = decoded.email;
    }
  }

  route() {
    this.router.navigate(['./login'])
  }


  switchBetweenLoginAndProducts() {
    if (this.currentRoute == '/login') {
      this.router.navigate(['./products'])
    } else if (this.currentRoute == '/products') {
      this.router.navigate(['./login'])
    }
  }

  logOut() {
    this.router.navigate(['login']).then(() => {
      this.snackBar.open('You have now been logged out', undefined, {duration: 3000})
      localStorage.clear();
      this.http.userName = undefined;
    })
  }
}
