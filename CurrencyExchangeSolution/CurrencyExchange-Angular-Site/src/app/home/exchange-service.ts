import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService } from '../base.service';
import { Observable } from 'rxjs';

import { Money } from './money';
import { Currency } from './currency';
import { ExchangeCard } from './exchange-card';

@Injectable({
  providedIn: 'root'
})
export class ExchangeService extends BaseService {

  constructor(http: HttpClient) {
    super(http);
  }

  getCurrencies(): Observable<Currency[]> {
    var url = this.getUrl("currency");
    return this.http.get<Currency[]>(url);
  }

  exchange(from: string, value: number, to: string): Observable<ExchangeCard> {
    var params = new HttpParams()
      .set("from", from)
      .set("to", to)
      .set("value", value);
    var url = this.getUrl("currency/exchange");
    return this.http.get<ExchangeCard>(url, { params });
  }


}
