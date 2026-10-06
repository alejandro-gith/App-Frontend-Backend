import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UserResponse } from '../../models/user-response';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-list',
  standalone: true,
  templateUrl: './user-list.html',
  imports: [

    RouterLink,
  ],
  styleUrl: './user-list.css'
})
export class UserList implements OnInit {
  users: UserResponse[] = [];
  loading = false;
  error = '';

  constructor(
    private userService: UserService,
    private changeDetector: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.loading = true;
    this.error = '';

    this.userService.getAll().subscribe({
      next: (users) => {
        this.users = users;
        this.loading = false;
        this.changeDetector.markForCheck();
      },
      error: () => {
        this.error = 'No se pudieron cargar los usuarios. Intenta nuevamente.';
        this.loading = false;
        this.changeDetector.markForCheck();
      }
    });
  }
}