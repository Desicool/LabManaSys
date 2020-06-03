import {
  Card,
  Descriptions,
  Table,
  Button,
} from 'antd';
import { GridContent, PageHeaderWrapper } from '@ant-design/pro-layout';
import React, { FC, useEffect } from 'react';
import { connect, Dispatch, UserModelState, IUser } from 'umi';
import styles from './index.less';
import { FinancialProcessModelState } from './model';
import { IFinancialForm } from '@/models/entity';

interface DeclarationProcessProps {
  loading: boolean;
  form?: IFinancialForm;
  formid: string;
  currentUser?: IUser;
  dispatch: Dispatch;
}

const DeclarationProcess: FC<DeclarationProcessProps> = (props) => {
  const { dispatch, form, formid, currentUser } = props;
  useEffect(() => {
    dispatch({
      type: 'financialProcess/fetch',
      payload: {
        formid: formid
      }
    });
  }, [formid]);
  return (
    <PageHeaderWrapper
      title="财务申请详情"
    >
      <div className={styles.main}>
        <GridContent>
          <Card style={{ marginBottom: 24 }} bordered={false} title={'表单'} >

            <Descriptions bordered>
              <Descriptions.Item label="申请编号">{form?.id}</Descriptions.Item>
              <Descriptions.Item label="所属实验室编号">{form?.lid}</Descriptions.Item>
              <Descriptions.Item label="提交时间">{form?.stime}</Descriptions.Item>
              <Descriptions.Item label="申请人姓名">{form?.uname}</Descriptions.Item>
              <Descriptions.Item label="当前状态">{form?.state}</Descriptions.Item>
              <Descriptions.Item label="金额">{form?.price}</Descriptions.Item>

            </Descriptions>
          </Card>
        </GridContent>
      </div>
      <div>
        <Button type="primary"
          style={{ width: '40%', marginRight: '3em', marginLeft: '10%' }}
          onClick={() => {
            var fidd : number = +formid;
            dispatch({
              type: 'financialProcess/approve',
              payload: {
                uid: currentUser?.userId,
                uname: currentUser?.realName,
                lid: currentUser?.labId,
                fid: parseInt(formid)
              }
            })
          }}>
          同意
        </Button>
        <Button type="primary" style={{ width: '40%' }}
          onClick={() => {
            dispatch({
              type: 'financialProcess/reject',
              payload: {
                uid: currentUser?.userId,
                uname: currentUser?.realName,
                lid: currentUser?.labId,
                fid: parseInt(formid)
              }
            })
          }}>
          驳回
        </Button>
      </div>
    </PageHeaderWrapper>
  );
}

export default connect(
  ({
    financialProcess,
    user,
  }: {
    financialProcess: FinancialProcessModelState;
    user: UserModelState;
  }, ownProps: any) => ({
    currentUser: user.currentUser,
    formid: ownProps.match.params.formid,
    form: financialProcess.form,
  }),
)(DeclarationProcess);
