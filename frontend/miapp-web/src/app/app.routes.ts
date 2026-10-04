import { Routes } from '@angular/router';

import { Login } from './features/auth/pages/login/login';
import { ProductList } from './features/products/pages/product-list/product-list';
import { authGuard } from './core/guards/auth-guard';
import { adminGuard } from './core/guards/admin-guard'; // US06
import { ProductFormComponent } from './features/products/pages/product-form/product-form.component'; // US06

import { CatalogComponent } from './features/products/pages/product-list/catalog.component';
import { DetailComponent } from './features/products/pages/product-list/detail/detail.component';

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
    path: 'catalog',
    component: CatalogComponent,
    canActivate: [authGuard]
  },
  // US06: debe ir ANTES de 'catalog/:id', si no Angular interpretaría "new" como un ID.
  // Exige sesión (authGuard) y rol Administrator (adminGuard).
  {
    path: 'catalog/new',
    component: ProductFormComponent,
    canActivate: [authGuard, adminGuard]
  },
  {
    path: 'catalog/:id',
    component: DetailComponent,
    canActivate: [authGuard]
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