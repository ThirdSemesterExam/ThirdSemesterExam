import {Component, OnInit, Pipe, PipeTransform} from '@angular/core';
import {HttpService} from "../../services/http.service";

@Component({
  selector: 'app-pets',
  templateUrl: './pet.component.html',
  styleUrls: ['./pet.component.scss']
})
export class PetComponent {

  constructor(public http: HttpService) {}
  hide = true;
  Pets: any[] = [];

  petName: string = "";
  petPrice: number = 0;
  petDescription: string = "";
  dogBreeds: string = "";
  address: string = "";
  zipcode:number = 0;
  city: string = "";
  email: string = "";
  image: string = "";


  toggleExpand(product: any) {
    product.expanded = !product.expanded;
  }
}

