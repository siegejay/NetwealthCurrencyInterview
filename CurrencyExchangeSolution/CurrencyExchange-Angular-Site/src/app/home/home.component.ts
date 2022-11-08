import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl, AsyncValidatorFn } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';


import { ExchangeService } from './exchange-service';
import { Currency } from './currency';
import { ExchangeCard } from './exchange-card';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  // The view title
  title?: string;

  // the form model
  form!: FormGroup;

  // the exchange result
  exchangeResult!: ExchangeCard; 

  // the currencies observable for the select (using async pipe)
  currencies?: Observable<Currency[]>;

  constructor(private exchangeService: ExchangeService) { }

  ngOnInit() {
    this.form = new FormGroup({
      from: new FormControl('', Validators.required),
      to: new FormControl('', Validators.required),
      value: new FormControl('', Validators.required)
    });

    this.loadData();
  }

  loadData() {
    this.loadCurrencies();
  }

  loadCurrencies() {
    // fetch all currencies from the server
    this.currencies = this.exchangeService
      .getCurrencies()
      .pipe(map(x => x));
  }

  exchangeValue() {
    var from = this.form.get('from')?.value;
    var value = this.form.get('value')?.value;
    var to = this.form.get('to')?.value;
    this.exchangeService
      .exchange(from, value, to)
      .subscribe(result => this.exchangeResult = result, error => console.error(error));
  }
}
