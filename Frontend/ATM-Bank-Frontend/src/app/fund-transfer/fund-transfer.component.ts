import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FundTransferService } from './fund-transfer.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-fund-transfer',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: 'fund-transfer.html',
  styleUrl: 'fund-transfer.css'
})
export class FundTransferComponent {

  fromAccount: string = '';
  toAccount: string = '';
  amount: number | null = null;
  description: string = '';

  message: string = '';
  errorMessage: string = '';
  isLoading: boolean = false;

  constructor(
    private fundTransferService: FundTransferService,
    private router:Router,
    private cdr:ChangeDetectorRef
  ) {
  }

  transfer(): void {

    this.message = '';
    this.errorMessage = '';

    if (!this.fromAccount || !this.toAccount || !this.amount) {
      this.errorMessage = 'Please enter all required details.';
      return;
    }

    if (this.amount <= 0) {
      this.errorMessage = 'Amount must be greater than 0.';
      return;
    }

    const transferData = {
      fromAccountNumber: Number(this.fromAccount),
      toAccountNumber: Number(this.toAccount),
      amount: this.amount,
      description: this.description
    };

    this.isLoading = true;

    this.fundTransferService
      .fundTransfer(transferData)
      .subscribe({

        next: (response) => {

          console.log('Fund Transfer Success:', response);

          this.message =
            'Fund transfer completed successfully.';

          this.fromAccount = '';
          this.toAccount = '';
          this.amount = null;
          this.description = '';

          this.isLoading = false;
          this.cdr.detectChanges();
        },

        error: (error) => {

          console.error(
            'Fund Transfer Error:',
            error
          );

          this.errorMessage =
            error?.error?.message ||
            'Fund transfer failed.';

          this.isLoading = false;
          this.cdr.detectChanges();
        }

        });
    }
     goToDashboard(): void {
     this.router.navigate(['/dashboard']);
}
}