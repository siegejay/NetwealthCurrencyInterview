import { Money } from './money';

export interface ExchangeCard {
  from?: Money,
  to?: Money,
  rateApplied?: number,
  notes?: string
}
