import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';

export const routes: Routes = [
  {
    path: '',
    component: LoginComponent
  },
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
  path: 'fund-transfer',
  loadComponent: () =>
    import('./fund-transfer/fund-transfer.component')
      .then(m => m.FundTransferComponent)
}
];