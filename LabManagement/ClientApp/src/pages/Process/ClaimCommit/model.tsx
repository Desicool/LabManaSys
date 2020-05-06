import { Effect, Reducer, IPostClaimFormParam } from 'umi';
import { queryClaimDetail, approveClaim, rejectClaim } from './service';
import { message } from 'antd';
import { IClaimForm, IChemical } from '@/models/entity';
import moment from 'moment';

export interface ClaimProcessModelState {
    data?: IPostClaimFormParam;
}

export interface ClaimProcessModelType {
    namespace: string;
    state: ClaimProcessModelState;
    effects: {
        fetch: Effect;
        approve: Effect;
        reject: Effect;
    };
    reducers: {
        fetchSuccess: Reducer<ClaimProcessModelState>;
    };
}

const Model: ClaimProcessModelType = {
    namespace: 'claimProcess',

    state: {
    },

    effects: {
        *fetch({ payload }, { call, put }) {
            const response : {
                form: IClaimForm;
                chemicals: IChemical[];
            } = yield call(queryClaimDetail, payload.formid);
            yield put({
                type: 'fetchSuccess',
                payload: {
                    ...response,
                    form: {
                        ...response.form,
                        stime: moment(response.form.stime).format('YYYY-MM-DD HH:mm:ss'),
                        rtime: moment(response.form.rtime).format('YYYY-MM-DD HH:mm:ss'),
                    }
                },
            });
        },
        *approve({ payload }, { call }) {
            yield call(approveClaim, payload);
            message.success('提交成功');
        },
        *reject({ payload }, { call }) {
            yield call(rejectClaim, payload);
            message.success('提交成功');
        }
    },

    reducers: {
        fetchSuccess(state, action) {
            return {
                ...state,
                data: action.payload,
            };
        },
    },
};

export default Model;
