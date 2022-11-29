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

  private readonly CurrencyRegExValidator = Validators.pattern(/^[-]?[0-9]+(\.[0-9]{1,2})?$/);

  // The view title
  title?: string = 'Code Demo Currency Exchange Calculator';

  // the form model
  form!: FormGroup;

  // Form Submitted state
  formIsSubmitted: boolean = false;

  // the exchange result
  exchangeResult!: ExchangeCard;

  // the currencies observable for the select (using async pipe)
  currencies?: Observable<Currency[]>;

  constructor(private exchangeService: ExchangeService) { }

  ngOnInit() {
    this.form = new FormGroup({
      from: new FormControl('', Validators.required),
      to: new FormControl('', Validators.required),
      value: new FormControl('', [Validators.required, this.CurrencyRegExValidator])
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
    this.formIsSubmitted = true;

    var from = this.form.get('from')?.value;
    var value = this.form.get('value')?.value;
    var to = this.form.get('to')?.value;
    this.exchangeService
      .exchange(from, value, to)
      .subscribe(result => this.exchangeResult = result, error => console.error(error));
  }

  getErrors(
    control: AbstractControl,
    displayName: string,
    customMessages: { [key: string]: string } | null = null
  ): string[] {
    var errors: string[] = [];
    Object.keys(control.errors || {}).forEach((key) => {
      switch (key) {
        case 'required':
          errors.push(`${displayName} ${customMessages?.[key] ?? "is required."}`);
          break;
        case 'pattern':
          errors.push(`${displayName} ${customMessages?.[key] ?? "contains invalid characters."}`)
          break;
        default:
          errors.push(`${displayName} is invalid.`)
          break;
      }
    });
    return errors;
  }

  get formCard() {
    return this.form.controls;
  }

  
}
