import {Component, OnInit, Pipe, PipeTransform} from '@angular/core';
import {HttpService} from "../../services/http.service";
import {FormControl, Validators} from "@angular/forms";

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

  getErrorMessage() {

  }
}
@Pipe({
  name: 'filter',
  pure: false
})
export class FilterPipe implements PipeTransform {
  transform(items: any[], searchText: string): any[] {
    if (!items) { return []; }
      return items.filter(item => {
        if (item) {
          return (item["name"].toLowerCase().includes(searchText));
        }
        return false;
      });
  }

}
