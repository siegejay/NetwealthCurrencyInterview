import { Component, OnInit } from '@angular/core';
import { ConnectionService } from 'angular-connection-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'CurrencyExchange-Angular-Site';

  hasNetworkConnection: boolean = true;
  hasInternetAccess: boolean = true;

  constructor(private connectionService: ConnectionService) {
    this.connectionService.monitor().subscribe((currentState: any) => {
      this.hasNetworkConnection = currentState.hasNetworkConnection;
      this.hasInternetAccess = currentState.hasInternetAccess;
    });
  }

  ngOnInit(): void { }

  public isOnline() {
    return this.hasNetworkConnection && this.hasInternetAccess;
  }
}

