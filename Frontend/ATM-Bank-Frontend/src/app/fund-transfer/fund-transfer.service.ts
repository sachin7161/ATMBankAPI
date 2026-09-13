import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FundTransferService {

  private apiUrl = 'https://localhost:7254/api/Transactions';

  constructor(private http: HttpClient) {
  }

  fundTransfer(data: any): Observable<any> {

    return this.http.post<any>(
     `${this.apiUrl}/FundTrnsfer`,
      data
    );
  }
}