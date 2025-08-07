import { Routes } from '@angular/router';

export const routes: Routes = [
  // Default route redirect to dashboard
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  
  // Lazy loaded feature modules
  {
    path: 'dashboard',
    loadChildren: () => import('./features/dashboard/dashboard.module').then(m => m.DashboardModule)
  },
  {
    path: 'applications',
    loadChildren: () => import('./features/applications/applications.module').then(m => m.ApplicationsModule)
  },
  {
    path: 'waiting-list',
    loadChildren: () => import('./features/waiting-list/waiting-list.module').then(m => m.WaitingListModule)
  },
  {
    path: 'admin',
    loadChildren: () => import('./features/admin/admin.module').then(m => m.AdminModule)
  },
  
  // Wildcard route for 404 page
  { path: '**', redirectTo: '/dashboard' }
];
