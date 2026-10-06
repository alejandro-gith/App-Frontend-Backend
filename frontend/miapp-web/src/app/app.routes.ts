import { Routes } from '@angular/router';

import { Login } from './features/auth/pages/login/login';
import { ProductList } from './features/products/pages/product-list/product-list';
import { CartDetailComponent } from './features/cart/pages/cart-detail/cart-detail.component';

import { authGuard } from './core/guards/auth-guard';
import { adminGuard } from './core/guards/admin-guard';

import { ProductFormComponent } from './features/products/pages/product-form/product-form.component';
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

  // US09 - US10: carrito
  {
    path: 'cart',
    component: CartDetailComponent,
    canActivate: [authGuard]
  },

  // US03 - US05: catálogo
  {
    path: 'catalog',
    component: CatalogComponent,
    canActivate: [authGuard]
  },

  // US06: crear producto
  // Debe ir antes de catalog/:id
  {
    path: 'catalog/new',
    component: ProductFormComponent,
    canActivate: [authGuard, adminGuard]
  },

  // US07: editar producto
  {
    path: 'catalog/:id/edit',
    component: ProductFormComponent,
    canActivate: [authGuard, adminGuard]
  },

  // US05: detalle del producto
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