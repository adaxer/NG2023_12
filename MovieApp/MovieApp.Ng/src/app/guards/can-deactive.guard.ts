import { CanDeactivateFn } from '@angular/router';
import { CanBeDirty } from '../models/can-be-dirty';

export const canDeactiveGuard: CanDeactivateFn<unknown> = (component, currentRoute, currentState, nextState) => {
  let canBeDirty = component as CanBeDirty;
  return (canBeDirty === undefined)
    ? true
    : !canBeDirty.isDirty;
};
