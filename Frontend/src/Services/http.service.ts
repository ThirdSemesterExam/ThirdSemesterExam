import {Injectable, OnInit} from '@angular/core';
import axios from 'axios';
import {MatSnackBar} from "@angular/material/snack-bar";
import {catchError} from "rxjs";
import {ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot} from "@angular/router";
import jwtDecode from "jwt-decode";

export const customAxios = axios.create({
  baseURL: 'http://localhost:5001'
})

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  pets: Pets[] = [];
  userName: any;


  constructor(private matSnackbar: MatSnackBar,
              private router: Router) {
    customAxios.interceptors.response.use(
      response => {
        if (response.status == 201) {
          this.matSnackbar.open("Great success", undefined, {duration: 2000})
        }
        return response;
      }, rejected => {
        if (rejected.response.status >= 400 && rejected.response.status < 500) {
          matSnackbar.open(rejected.response.data);
        } else if (rejected.response.status > 499) {
          this.matSnackbar.open("Something went wrong", "error", {duration: 3000})
        }
        catchError(rejected);
      }
    );
    customAxios.interceptors.request.use(
      async config => {
        if (localStorage.getItem('token')) {
          config.headers = {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        }

        return config;
      },
      error => {
        Promise.reject(error)
      });
  }


  getPets() {
    customAxios.get<Pets[]>('pet').then(success => {
      console.log(success);
      this.pets = success.data;
    }).catch(e => {
      console.log(e);
    })
    console.log('now were are executing this');
  }


  async login(dto: any) {
    customAxios.post<string>('auth/login', dto).then(successResult => {
      localStorage.setItem('token', successResult.data);
      let t = jwtDecode(successResult.data) as User;
      this.userName = t.email;
      this.router.navigate(['./pets'])
      this.matSnackbar.open("Welcome", undefined, {duration: 3000})
    })
  }


  async register(param: { role: string; password: any; email: any }) {
    customAxios.post('auth/register', param).then(successResult => {
      localStorage.setItem('token', successResult.data);
      this.router.navigate(['./pets'])
      this.matSnackbar.open("You have been registered", undefined, {duration: 3000});
    })
  }
}


interface Pets {
  id: number,
  name: string,
  price: number,
  description: string,
  dogBreeds: string,
  address: string,
  zipcode:number,
  city: string,
  email: string,
  image: string,
  expanded: boolean
}

interface User {
  email: string
}


@Injectable({providedIn: 'root'})
export class MyResolver implements Resolve<any> {
  constructor(private http: HttpService) {
  }


  async resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<any> {
    await this.http.getPets();
    return true;
  }
}
