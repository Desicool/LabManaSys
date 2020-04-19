import {
  Card,
  Statistic,
  Descriptions,
  Divider,
  Tooltip,
  Empty,
  Table,
  Badge,
  Button,
  List,
} from 'antd';
import { GridContent, PageHeaderWrapper, RouteContext } from '@ant-design/pro-layout';
import React, { FC, useEffect, useState } from 'react';
import { connect, Dispatch, UserModelState, IUser } from 'umi';
import styles from './style.less';
import { DeclarationProcessModelState } from './model';
import { IDeclarationForm, IChemical } from '@/models/entity';

interface DeclarationProcessProps {
  loading: boolean;
  detail?: {
    form: IDeclarationForm;
    chemicals: IChemical[];
  }
  formid: number;
  currentUser?: IUser;
  dispatch: Dispatch;
}

const DeclarationProcess: FC<DeclarationProcessProps> = (props) => {
  const { dispatch, detail, formid, currentUser } = props;
  useEffect(() => {
    dispatch({
      type: 'declarationProcess/fetch',
      payload: {
        formid: 1
      }
    });
  }, []);
  const columns = [
    {
      title: '编号',
      dataIndex: 'id',
      width: '6em'
    },
    {
      title: '名称',
      dataIndex: 'name',
      width: '8em'
    },
    {
      title: '数量',
      dataIndex: 'amount'
    },
    {
      title: '单价',
      dataIndex: 'unitprice',
    }
  ]
  return (
    <PageHeaderWrapper
      title="采购申请详情"
    >
      <div className={styles.main}>
        <GridContent>
          <Card style={{ marginBottom: 24 }} bordered={false} title={'表单'} >

            <Descriptions bordered>
              <Descriptions.Item label="申请编号">{detail?.form.id}</Descriptions.Item>
              <Descriptions.Item label="所属实验室编号">{detail?.form.lid}</Descriptions.Item>
              <Descriptions.Item label="提交时间">{detail?.form.stime}</Descriptions.Item>
              <Descriptions.Item label="当前状态">{detail?.form.state}</Descriptions.Item>
              <Descriptions.Item label="申请理由" span={2}>{detail?.form.reason}</Descriptions.Item>

            </Descriptions>
          </Card>
          <Card style={{ marginBottom: 24 }} bordered={false} title={'化学危险品列表'} >
            <Table
              dataSource={detail?.chemicals.map(u => ({ ...u, key: u.id }))}
              columns={columns}
              bordered
            />
          </Card>
        </GridContent>
      </div>
      <div>
        <Button type="primary"
          style={{ width: '40%', marginRight: '3em', marginLeft: '10%' }}
          onClick={() => {
            dispatch({
              type: 'declarationProcess/approve',
              payload: {
                uid: currentUser?.userId,
                uname: currentUser?.userName,
                lid: currentUser?.labId,
                fid: detail?.form.id
              }
            })
          }}>
          同意
        </Button>
        <Button type="primary" style={{ width: '40%' }}
          onClick={() => {
            dispatch({
              type: 'declarationProcess/reject',
              payload: {
                uid: currentUser?.userId,
                uname: currentUser?.userName,
                lid: currentUser?.labId,
                fid: detail?.form.id
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
    declarationProcess,
    loading,
    user,
  }: {
    declarationProcess: DeclarationProcessModelState;
    user: UserModelState;
    loading: {
      effects: { [key: string]: boolean };
    };
  }, ownProps: any) => ({
    currentUser: user.currentUser,
    formid: ownProps.match.params.formid,
    detail: declarationProcess.detail,
    loading: loading.effects['myDeclarationProcess/fetchAdvanced'],
  }),
)(DeclarationProcess);
