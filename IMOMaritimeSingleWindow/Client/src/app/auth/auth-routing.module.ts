import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthHomeComponent } from './auth-home/auth-home.component';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { AuthHomeGuard } from './guards/auth-home.guard';
import { EmailConfirmationGuard } from './guards/email-confirmation.guard';
import { LoginGuard } from './guards/login.guard';
import { PasswordResetGuard } from './guards/password-reset.guard';
import { LoginComponent } from './login/login.component';
import { PasswordChangeComponent } from './password-change/password-change.component';
import { PasswordForgottenComponent } from './password-forgotten/password-forgotten.component';
import { PasswordResetComponent } from './password-reset/password-reset.component';

const authRoutes: Routes = [
  {
    path: '',   /*  /auth   */
    // redirectTo: 'auth/login',
    // pathMatch: 'full',
    // component: LoginComponent,
    // canActivate: [LoginGuard],
    component: AuthHomeComponent,
    children: [
        {
          path: '',
          pathMatch: 'full',
          redirectTo: 'login'
        },
        {
            path: 'ResetPassword',
            component: PasswordResetComponent,
            // canActivate: [PasswordResetGuard]
          },
          {
            path: 'ChangePassword',
            component: PasswordChangeComponent,
            data: { title: 'Change password'}
          },
          {
            path: 'ForgotPassword',
            component: PasswordForgottenComponent
          },
          {
            path: 'ConfirmEmail',
            component: EmailConfirmationComponent,
            canActivate: [EmailConfirmationGuard]
          },
          {
              path: 'login',
              component: LoginComponent,
              canActivate: [LoginGuard]
          }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(authRoutes)],
  exports: [RouterModule]
})
export class AuthRoutingModule {}
