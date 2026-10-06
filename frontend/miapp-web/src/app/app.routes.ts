import { Routes } from '@angular/router';

import { Login } from './features/auth/pages/login/login';
import { ProductList } from './features/products/pages/product-list/product-list';
import { authGuard } from './core/guards/auth-guard';

import { UserList } from './features/users/pages/user-list/user-list';
import { usersGuard } from './core/guards/users-guard';

export const routes: Routes = [
  {
    path: 'login',
    component: Login
  },
  {
    path: 'products',
    component: ProductList,
    canActivate: [authGuard]
  },
  {
    path: 'users',
    component: UserList,
    canActivate: [usersGuard]
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];