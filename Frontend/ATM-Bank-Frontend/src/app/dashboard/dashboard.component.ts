import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardService } from './dashboard.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: 'dashboard.html',
  styleUrl: 'dashboard.css'
})
export class DashboardComponent implements OnInit {

  userName: string = localStorage.getItem('userName') || '';
  role: string = localStorage.getItem('role') || '';

  balance: number = 0;

  accountNumber: string = 'Loading...';
  accountType: string = 'Loading...';
  accountStatus: string = 'Loading...';

  customerName: string = '';
  openDate: string = '';
  mobileNumber: string = '';
  email: string = '';
  address: string = '';

  cardNumber: string = 'XXXX XXXX XXXX XXXX';
  cardExpiry: string = '--/--';
  dailyLimit: number = 0;
  cardStatus: string = 'Not Available';
  recentTransactions: any[] = [];
  loans: any[] = [];

  constructor(
    private dashboardService: DashboardService,
    private cdr: ChangeDetectorRef,
    private router:Router
  ) {
  }

  ngOnInit(): void {

    this.dashboardService
      .getMyAccountNumber()
      .subscribe({

        next: (accountNumber) => {

          console.log(
            'My Account Number:',
            accountNumber
          );

          this.accountNumber =
            accountNumber.toString();

          this.dashboardService
            .getAccountDashboard(accountNumber)
            .subscribe({

              next: (response) => {

                console.log(
                  'Dashboard Data:',
                  response
                );

                // Account Details
                this.accountNumber =
                  response.account?.accountNumber?.toString()
                  || '';

                this.accountType =
                  response.account?.accountType
                  || '';

                this.accountStatus =
                  response.account?.status
                  || '';

                this.balance =
                  response.balance
                  || 0;
                 this.recentTransactions =
            response.recentTransactions || [];

            this.loans =
            response.loans || [];

                // Customer Details
                this.customerName =
                  response.customer?.customerName
                  || '';

                this.mobileNumber =
                  response.customer?.mobileNumber
                  || '';

                this.email =
                  response.customer?.email
                  || '';

                this.address =
                  response.customer?.address
                  || '';

                // Opening Date
                this.openDate =
                  response.account?.openDate
                    ? new Date(
                        response.account.openDate
                      ).toLocaleDateString()
                    : '';

                // ATM Card
                if (response.atmCard) {

                  this.cardNumber =
                    response.atmCard.cardNumber
                      ?.toString()
                    || 'XXXX XXXX XXXX XXXX';

                  this.cardExpiry =
                    response.atmCard.expiryDate
                    || '--/--';

                  this.dailyLimit =
                    response.atmCard.dailyLimit
                    || 0;

                  this.cardStatus =
                    response.atmCard.cardStatus
                    || 'Not Available';

                } else {

                  this.cardNumber =
                    'XXXX XXXX XXXX XXXX';

                  this.cardExpiry =
                    '--/--';

                  this.dailyLimit = 0;

                  this.cardStatus =
                    'Not Available';
                }

                console.log('UI Values:', {
                  accountNumber: this.accountNumber,
                  accountType: this.accountType,
                  balance: this.balance,
                  customerName: this.customerName,
                  mobileNumber: this.mobileNumber,
                  email: this.email
                });

                // Refresh UI
                this.cdr.detectChanges();
              },

              error: (error) => {

                console.error(
                  'Dashboard API Error:',
                  error
                );

              }

            });

        },

        error: (error) => {

          console.error(
            'Account Number API Error:',
            error
          );

        }

      });
  }

  goToFundTransfer(): void {
  this.router.navigate(['/fund-transfer']);
}


  logout(): void {

    localStorage.clear();

    window.location.href = '/';
  }
}